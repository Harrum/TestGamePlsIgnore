using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.game.entitites;
using TestGamePleaseIgnore.src.game.entitites.blocks;
using Bitmap = System.Drawing.Bitmap;
using Image = System.Drawing.Image;

namespace TestGamePleaseIgnore.src.game
{
    public class Level
    {
        private String LevelName;
        private RectangleF levelBounds;

        public Level()
        {

        }

        public void ResetLevel()
        {
            Game.ClearEntities();
            LoadLevel(LevelName);
        }

        public RectangleF GetBounds()
        {
            return this.levelBounds;
        }

        public void LoadLevel(String lvlName)
        {
            this.LevelName = lvlName;

            Bitmap bmp = (Bitmap)Image.FromFile(Resources.RESOURCE_PATH + "\\src\\game\\levels\\" + lvlName + ".bmp");
            levelBounds = new RectangleF(0, 0, bmp.Width * Block.WIDTH, bmp.Height * Block.HEIGHT);

            for(int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    string hexColor = bmp.GetPixel(x, y).Name.ToUpper();

                    if (hexColor != "FFFF00FF")   //Check if the pixel does not represent an empty tile (pink)
                    {
                        BaseEntity ent;
                        if (hexColor == "FF5EF6FF")
                        {
                            ent = new FallingBlock(x * Block.WIDTH, y * Block.HEIGHT, GameTextures.IceBlock);
                            Game.AddCollidableEntity(ent);
                        }
                        else if (hexColor == "FFFF2340")
                        {
                            ent = new BouncyBlock(x * Block.WIDTH, y * Block.HEIGHT, GameTextures.ArrowRight);
                            Game.AddCollidableEntity(ent);
                        }
                        else if (hexColor == "FF000000")
                        {
                            ent = new Penguin(x * Block.WIDTH, y * Block.HEIGHT);
                            Camera.GetInstance().SetActor(ent);
                            EntityController.GetInstance().SetPuppetEntity((ControllableEntity)ent);
                            Game.AddCollidableEntity(ent);
                        }
                        else if (hexColor == "FFFFFFFF")
                        {
                            ent = new Block(x * Block.WIDTH, y * Block.HEIGHT, GameTextures.SnowBlock);
                            Game.AddEntity(ent);
                        }
                        else
                        { 
                            ent = new Block(x * Block.WIDTH, y * Block.HEIGHT, GameTextures.UnknowEntity);
                            Game.AddEntity(ent);
                        }
                    }
                }
            }
        }
    }
}
