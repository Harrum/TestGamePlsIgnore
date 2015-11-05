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
        private static List<BaseEntity> Entities;
        private List<BaseEntity> VisibleEntities;
        private static List<BaseEntity> CollidableEntities;
        private Camera GameCamera;
        QuadTree Quad;

        public void InitBase()
        {
            VIEWPORT = new ViewportF(0, 0, Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT);
            Entities = new List<BaseEntity>();
            VisibleEntities = new List<BaseEntity>();
            CollidableEntities = new List<BaseEntity>();
            Initialize();
            GameCamera = Camera.GetInstance();
            Quad = new QuadTree(0, new RectangleF(VIEWPORT.X, VIEWPORT.Y, VIEWPORT.Width, VIEWPORT.Height));
        }

        public abstract void Initialize();

        public abstract void LoadContent(RenderTarget g);

        /// <summary>
        /// Updates the viewport, creates a list wich contains all the visible entites inside the viewport.
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
                if(VIEWPORT.Bounds.Contains(e.X, e.Y))
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
            for(int i = 0; i < CollidableEntities.Count; i++)
            {
                returnObjects.Clear();
                Quad.Retrieve(returnObjects, CollidableEntities[i].Hitbox);
                returnObjects.Remove(CollidableEntities[i]);
                CollidableEntities[i].HandleCollisions(returnObjects);
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
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(elapsedTime);
            }
            UpdateQuadTree();
            UpdateCollisions();
            GameCamera.Update();
        }

        /// <summary>
        /// Draws all the entities wich are visible in the viewport
        /// </summary>
        /// <param name="g">The RenderTarget used to perform the drawing operations.</param>
        public virtual void Draw(RenderTarget g)
        {
            UpdateViewPort();
            for (int i = 0; i < VisibleEntities.Count; i++)
            {
                VisibleEntities[i].Draw(g);
            }
            if (Config.DEBUG_MODE == DebugMode.DISPLAY_HITBOX)
            {
                Quad.Draw(g);
            }
        }
    }
}
