using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    class HUD2
    {
        Player player;
        SpriteBatch spriteBatch;
        Texture2D heart;
        SpriteFont font;

        public HUD2(Player player,SpriteFont font,Texture2D tex,SpriteBatch spriteBatch)
        {
            

            this.player = player;
            this.heart = tex;
            this.font = font;
            this.spriteBatch = spriteBatch;
            //IsPopup = true;

            //this.ScreenState = ScreenState.TransitionOff;
            

        }
        
        public void draw()
        {
            
            Rectangle viewport = new Rectangle(5, 5, 40, 40);
            //byte fade = TransitionAlpha;
            
            Vector2 fontPos = new Vector2(50, 5);

            spriteBatch.Begin();


            spriteBatch.Draw(heart, viewport,
                             new Color(0, 0, 0));
            spriteBatch.DrawString(font, "Lives: " + player.getLives(), fontPos,
                Color.Yellow);



            spriteBatch.End();
            
        }
    }
}
