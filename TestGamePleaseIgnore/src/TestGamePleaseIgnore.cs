using SharpDX;
using SharpDX.Windows;
using Factory = SharpDX.Direct2D1.Factory;
using Device1 = SharpDX.Direct3D10.Device1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using System.Threading;

namespace TestGamePleaseIgnore.src
{
    public class TestGamePleaseIgnore
    {
        private const long MS_PER_UPDATE = TimeSpan.TicksPerSecond / 100;
        private RenderForm form;
        private Factory factory2D;
        private RenderTarget renderTarget;
        private Texture2D backBuffer;
        RenderTargetView backBufferView;
        private Device1 device;
        private SwapChain swapChain;

        private Thread thread;
        private bool isRunning;
        private int fps;
        private string title;

        //private Textures textures;
        private RunnableComponent runnableComponent;
        private InputController input;

        public TestGamePleaseIgnore(string title, RunnableComponent runnableComponent)
        {
            this.isRunning = false;
            this.fps = 0;
            this.title = title;
            this.runnableComponent = runnableComponent;
            //textures = new Textures();
            input = new InputController();
        }

        private RenderForm CreateForm()
        {
            RenderForm renderForm = new RenderForm("Game of Awesome");
            renderForm.ClientSize = new System.Drawing.Size((int)Config.SCREEN_WIDTH, (int)Config.SCREEN_HEIGHT);
            renderForm.AllowUserResizing = false;
            renderForm.KeyDown += input.KeyDown;
            renderForm.KeyUp += input.KeyUp;
            return renderForm;
        }

        private void Initialize()
        {
            // Create the form
            form = CreateForm();

            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription((int)Config.SCREEN_WIDTH, (int)Config.SCREEN_HEIGHT,
                                        new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            //Create the device and swapchain
            try
            {
                Device1.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, desc, out device, out swapChain);
            }
            catch (Exception e)
            {
                Device1.CreateWithSwapChain(DriverType.Warp, DeviceCreationFlags.BgraSupport, desc, out device, out swapChain);
                Console.WriteLine("Could not create Hardware drivertype, using Warp instead. \n" + e.ToString());
            }

            // Ignore all Windows events
            SharpDX.DXGI.Factory factory = swapChain.GetParent<SharpDX.DXGI.Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            backBufferView = new RenderTargetView(device, backBuffer);

            // Create the rendertarget for the form
            factory2D = new SharpDX.Direct2D1.Factory();
            Surface surface = backBuffer.QueryInterface<Surface>();
            renderTarget = new RenderTarget(factory2D, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            renderTarget.AntialiasMode = AntialiasMode.Aliased;
            // Initialize the global resources used for drawing and writing.
            Resources.Initialize(renderTarget);
            runnableComponent.InitBase();
        }

        private void LoadContent()
        {
            runnableComponent.LoadContent(renderTarget);
        }

        private void Update(long elapsedTime)
        {
            runnableComponent.Update(elapsedTime);
            input.Update();
        }

        private void Render()
        {
            form.Text = title + " - FPS: " + fps;
            renderTarget.BeginDraw();
            renderTarget.Clear(Color.Blue);

            //Drawing here
            runnableComponent.Draw(renderTarget);
            renderTarget.DrawText("FPS: " + fps, Resources.TEXT_FORMAT, Config.SCREEN_RECT, Resources.SCBRUSH_RED);
            renderTarget.EndDraw();
            swapChain.Present(0, PresentFlags.None);
        }

        public void Start()
        {
            if (isRunning) return;
            isRunning = true;
            //Draw();
            thread = new Thread(new ThreadStart(this.Run));
            thread.Start();
        }

        public void Stop()
        {
            if (!isRunning) return;
            isRunning = false;
            //thread = null;
            //thread.Join();
        }

        public void Run()
        {
            Initialize();
            LoadContent();

            int frames = 0;
            long previousTime = DateTime.Now.Ticks;
            long totalElapsedTime = 0;
            long ticks = 0;
            long lag = 0;

            RenderLoop.Run(form, () =>
            {
                long currentTime = DateTime.Now.Ticks;
                long elapsedTime = currentTime - previousTime;
                previousTime = currentTime;
                lag += elapsedTime;
                totalElapsedTime += elapsedTime;
                ticks += elapsedTime;

                while (lag >= MS_PER_UPDATE)
                {
                    Update(totalElapsedTime);
                    lag -= MS_PER_UPDATE;

                    if (ticks / TimeSpan.TicksPerSecond >= 1)
                    {
                        this.fps = frames;
                        frames = 0;
                        ticks = 0;
                    }
                    totalElapsedTime = 0;
                }

                Render();
                frames++;
            });
        }
    }
}
