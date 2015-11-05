using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.game.entitites.blocks;

namespace TestGamePleaseIgnore.src.game
{
    public class Level
    {
        String LevelName;

        public Level()
        {

        }

        public void ResetLevel()
        {
            Game.ClearEntities();
            LoadLevel(LevelName);
        }

        public void LoadLevel(String lvlName)
        {
            this.LevelName = lvlName;

            Bitmap bmp = (Bitmap)Image.FromFile(Resources.RESOURCE_PATH + "\\src\\game\\levels\\" + lvlName + ".bmp");

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
