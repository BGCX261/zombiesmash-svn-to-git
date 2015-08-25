using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieSmashGame.GameScreens
{
    class ScreenManager
    {
        static Dictionary<String,GameScreen> m_screens = new Dictionary<String,GameScreen>();
        static GameScreen m_actual = null;

        public static GameScreen Actual
        {
            get { return ScreenManager.m_actual; }
            set { ScreenManager.m_actual = value; }
        }


        public static void AddScreen(GameScreen screen, String name)
        {
            m_screens.Add(name,screen);
        }

        public static void SwitchScreen(String name)
        {
            if (m_actual != null)
            {
                if(m_screens[name] != m_actual)
                m_actual.UnloadContent(); // Unload current screen
            }


            if (m_screens[name] != null && m_actual != m_screens[name])
            {
                m_screens[name].LoadContent();
                m_actual = m_screens[name];
            }
        }

        public static void UpdateScreens(GameTime gameTime)
        {
            if(m_actual !=null)
            m_actual.Update(gameTime);
        }

        public static void RenderScreens(GameTime gameTime)
        {
            if (m_actual != null)
            m_actual.Render(gameTime);
        }

    }
}
