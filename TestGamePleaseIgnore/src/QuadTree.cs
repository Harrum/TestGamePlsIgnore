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
    /// <summary>
    /// Based on this article:
    /// http://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374 
    /// </summary>
    public class QuadTree
    {
        private const int MAX_OBJECTS = 10;
        private const int MAX_LEVELS = 5;

        private int Level;
        private List<BaseEntity> Objects;
        private RectangleF Bounds;
        private QuadTree[] Nodes;

        public QuadTree(int level, RectangleF bounds)
        {
            this.Level = level;
            this.Objects = new List<BaseEntity>();
            this.Bounds = bounds;
            this.Nodes = new QuadTree[4];
        }

        public void Draw(RenderTarget g)
        {
            g.DrawRectangle(Bounds, Resources.SCBRUSH_RED, 2f);
            foreach(BaseEntity ent in Objects)
            {
                g.DrawText(Level.ToString(), Resources.TEXT_FORMAT, ent.Hitbox, Resources.SCBRUSH_RED);
            }
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] != null)
                {
                    Nodes[i].Draw(g);
                }
            }
        }

        /// <summary>
        /// Updates the bounds of the quadtree to the new one specified.
        /// </summary>
        /// <param name="newBounds">The new bounds of the quadtree.</param>
        public void UpdateBounds(RectangleF newBounds)
        {
            this.Bounds = newBounds;
        }

        /// <summary>
        /// Clears the quadtree.
        /// </summary>
        public void Clear()
        {
            Objects.Clear();

            for(int i = 0; i <  Nodes.Length; i++)
            {
                if(Nodes[i] != null)
                {
                    Nodes[i].Clear();
                    Nodes[i] = null;
                }
            }
        }

        /// <summary>
        /// Splits the node into 4 subnodes.
        /// </summary>
        private void Split()
        {
            float subWidth = (Bounds.Width / 2);
            float subHeight = (Bounds.Height / 2);
            float x = Bounds.X;
            float y = Bounds.Y;

            Nodes[0] = new QuadTree(Level + 1, new RectangleF(x + subWidth, y, subWidth, subHeight));
            Nodes[1] = new QuadTree(Level + 1, new RectangleF(x, y, subWidth, subHeight));
            Nodes[2] = new QuadTree(Level + 1, new RectangleF(x, y + subHeight, subWidth, subHeight));
            Nodes[3] = new QuadTree(Level + 1, new RectangleF(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        /// <summary>
        /// Determine which node the object belongs to. -1 means
        /// object cannot completely fit within a child node and is part
        /// of the parent node
        /// </summary>
        /// <param name="hitbox"></param>
        /// <returns></returns>
        private int GetIndex(RectangleF hitbox)
        {
            int index = -1;
            float verticalMidpoint = Bounds.X + (Bounds.Width / 2f);
            float horizontalMidpoint = Bounds.Y + (Bounds.Height / 2f);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (hitbox.Top > Bounds.Top && hitbox.Bottom < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (hitbox.Top > horizontalMidpoint && hitbox.Bottom < Bounds.Bottom);

            // Object can completely fit within the left quadrants
            if (hitbox.Left > Bounds.Left && hitbox.Right < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (hitbox.X > verticalMidpoint && hitbox.Right < Bounds.Right)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /// <summary>
        /// Insert the object into the quadtree. if the node
        /// exceeds the capacity, it will split and add all 
        /// objects to their corresponding nodes.
        /// </summary>
        /// <param name="ent"></param>
        public void Insert(BaseEntity ent)
        {
            if(Nodes[0] != null)
            {
                int index = GetIndex(ent.Hitbox);

                if(index != -1)
                {
                    Nodes[index].Insert(ent);
                    return;
                }
            }

            Objects.Add(ent);

            if(Objects.Count > MAX_OBJECTS && Level < MAX_LEVELS)
            {
                if(Nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while(i < Objects.Count)
                {
                    int index = GetIndex(Objects[i].Hitbox);
                    if(index != -1)
                    {
                        Nodes[index].Insert(Objects[i]);
                        Objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Return all ojbects that could collide with the given object.
        /// </summary>
        /// <param name="returnObjects"></param>
        /// <param name="hitBox"></param>
        public void Retrieve(List<BaseEntity> returnObjects, RectangleF hitBox)
        {
            if (Nodes[0] != null)
            {
                var index = GetIndex(hitBox);
                if (index != -1)
                {
                    Nodes[index].Retrieve(returnObjects, hitBox);
                }
                else
                {
                    for (int i = 0; i < Nodes.Length; i++)
                    {
                        Nodes[i].Retrieve(returnObjects, hitBox);
                    }
                }
            }
            returnObjects.AddRange(Objects);
        }

        /* this methode will only return the objects in the parent node if index = -1
           It can be used when checking collision on all items, but not when you check for
           a select few items as it wil not get the childs nodes items.
        /// <summary>
        /// Return all ojbects that could collide with the given object.
        /// </summary>
        /// <param name="returnObjects"></param>
        /// <param name="hitBox"></param>
        public void Retrieve(List<BaseEntity> returnObjects, RectangleF hitBox)
        {
            int index = GetIndex(hitBox);
            if(index != -1 && Nodes[0] != null)
            {
                Nodes[index].Retrieve(returnObjects, hitBox);
            }

            returnObjects.AddRange(Objects);
        }*/
    }
}
