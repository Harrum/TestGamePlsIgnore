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

        public void InitBase()
        {
            VIEWPORT = new ViewportF(0, 0, Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT);
            Entities = new List<BaseEntity>();
            VisibleEntities = new List<BaseEntity>();
            Initialize();
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

        /// <summary>
        /// Adds an entity to the game's render and update loop
        /// </summary>
        /// <param name="entity">The Entity to add</param>
        public static void AddEntity(BaseEntity entity)
        {
            Entities.Add(entity);
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
        }
    }
}
