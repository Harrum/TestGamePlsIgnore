using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src
{
    public abstract class RunnableComponent
    {
        public static ViewportF VIEWPORT;
        private RectangleF GAME_AREA_SIZE;

        private static List<BaseEntity> Entities;
        private List<BaseEntity> VisibleEntities;
        private static List<BaseEntity> CollidableEntities;
        private Camera GameCamera;
        QuadTree Quad;
        
        Matrix3x2 ViewportIDMatrix;
        Matrix3x2 ViewportIDMatrixMirror;

        public void InitBase()
        {
            VIEWPORT = new ViewportF(0, 0, Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT);
            GAME_AREA_SIZE = new RectangleF(0, 0, Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT);
            ViewportIDMatrix = Matrix.Identity;
            ViewportIDMatrixMirror = new Matrix3x2();
            ViewportIDMatrixMirror.M11 = -1; ViewportIDMatrixMirror.M12 = 0;
            ViewportIDMatrixMirror.M21 = 0; ViewportIDMatrixMirror.M22 = 1;
            ViewportIDMatrixMirror.M31 = 0; ViewportIDMatrixMirror.M32 = 0;


            Entities = new List<BaseEntity>();
            VisibleEntities = new List<BaseEntity>();
            CollidableEntities = new List<BaseEntity>();
            Initialize();
            GameCamera = Camera.GetInstance();
            Quad = new QuadTree(0, GAME_AREA_SIZE);
        }

        public abstract void Initialize();

        public abstract void LoadContent(RenderTarget g);

        protected void SetGameAreaSize(RectangleF area)
        {
            this.GAME_AREA_SIZE = area;
            Quad.UpdateBounds(GAME_AREA_SIZE);
        }

        /// <summary>
        /// Updates the viewport, creates a list which contains all the visible entites inside the viewport.
        /// The list is sorted by Entity.Layer so it wil perform drawing operations in layers,
        /// Layer 1 first, 2 second, etc.
        /// </summary>
        protected void UpdateViewPort()
        {
            VIEWPORT.X = GameCamera.X;
            VIEWPORT.Y = GameCamera.Y;
            VisibleEntities.Clear();
            foreach(BaseEntity e in Entities)
            {
                if(e.X + e.Width >= VIEWPORT.X && e.X <= VIEWPORT.X + VIEWPORT.Width
                    && e.Y + e.Height >= VIEWPORT.Y && e.Y <= VIEWPORT.Y + VIEWPORT.Height)
                {
                    VisibleEntities.Add(e);
                }
            }
            VisibleEntities = VisibleEntities.OrderBy(ent => ent.Layer).ToList<BaseEntity>();
        }

        private void UpdateQuadTree()
        {
            Quad.Clear();
            for(int i = 0; i < Entities.Count; i++)
            {
                Quad.Insert(Entities[i]);
            }
        }

        private void UpdateCollisions()
        {
            List<BaseEntity> returnObjects = new List<BaseEntity>();

            foreach(BaseEntity e in CollidableEntities)
            {
                returnObjects.Clear();
                Quad.Retrieve(returnObjects, e.Hitbox);
                returnObjects.Remove(e);
                e.HandleCollisions(returnObjects);
            }
        }

        public static void ClearEntities()
        {
            CollidableEntities.Clear();
            Entities.Clear();
        }

        /// <summary>
        /// Adds an entity to the game's render and update loop
        /// </summary>
        /// <param name="entity">The Entity to add</param>
        public static void AddEntity(BaseEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Adds an entity which needs to check for collision tot he game's render and update loop,
        /// By using this method it is not neccessary to use AddEntity too.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public static void AddCollidableEntity(BaseEntity entity)
        {
            Entities.Add(entity);
            CollidableEntities.Add(entity);
        }

        /// <summary>
        /// Updates all the entites.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time in ms since the last update</param>
        public virtual void Update(long elapsedTime)
        {
            UpdateViewPort();
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(elapsedTime);
            }
            UpdateQuadTree();
            UpdateCollisions();
            GameCamera.Update();
        }

        /// <summary>
        /// Set the matrix to the identity matrix transated to the viewport.
        /// This causes the screen to move along with the camera at the center.
        /// </summary>
        /// <param name="g">The rendertarget which holds the matrix.</param>
        public void SetMatrixViewportID(RenderTarget g)
        {
            Vector2 viewPortTranslation = new Vector2(-VIEWPORT.X, -VIEWPORT.Y);
            ViewportIDMatrix = Matrix.Identity;
            ViewportIDMatrix.TranslationVector = viewPortTranslation;
            g.Transform = ViewportIDMatrix;
        }

        /// <summary>
        /// Draws all the entities wich are visible in the viewport
        /// </summary>
        /// <param name="g">The RenderTarget used to perform the drawing operations.</param>
        public virtual void Draw(RenderTarget g)
        {
            //At the start set the matrix to the identy viewport matrix
            SetMatrixViewportID(g);

            //Start drawing all the entities.
            foreach (BaseEntity ent in VisibleEntities)
            {
                //If an entity is mirror the matrix needs to get mirrored too.
                if(ent.Mirrored)
                {
                    Vector2 mirroredviewPortTranslation = new Vector2(-VIEWPORT.X + ent.X + ent.Width + ent.X, -VIEWPORT.Y);
                    ViewportIDMatrixMirror.TranslationVector = mirroredviewPortTranslation;
                    g.Transform = ViewportIDMatrixMirror;
                }

                //Actual drawing of the entity
                ent.Draw(g);

                //If the entity was mirrored then the matrix needs to be reset back to the identity viewport matrix.
                if (ent.Mirrored)
                    SetMatrixViewportID(g);
            }
            if (Config.DEBUG_MODE == DebugMode.DISPLAY_HITBOX)
            {
                Quad.Draw(g);
            }
            g.Transform = Matrix.Identity;
        }
    }
}
