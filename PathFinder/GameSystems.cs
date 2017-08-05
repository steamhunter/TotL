using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public enum GameSystemType
    {
        map
    }

    public static class GameSystems
    {
        private static List<IGameSystem> gamesystems = new List<IGameSystem>();
        private static List<IGameSystem> mapsystems = new List<IGameSystem>();
        public static void Initialize()
        {
            //Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
           
            //foreach (var item in asms)
            //{
            //    var types = item.GetTypes().Where(x=>x.CustomAttributes.Count()>0).Where(y=>y.CustomAttributes.Where(z=>z.AttributeType.Name=="GameSystem").Count()>0);//.Where(x=>x.CustomAttributes.Count()>0).Where(y=>y.GetType().Name=="GameSystem");//.Where(x => x.CustomAttributes.ToList().Exists(y=>y.AttributeType.FullName==typeof(GameSystem).FullName));
                
            //    foreach (var type in types)
            //    {  

            //        if (!type.IsAbstract)
            //        {
            //            var instance = Activator.CreateInstance(type);
            //                gamesystems.Add(instance as IGameSystem);
            //        }

            //    }
                
            //}
            //Console.WriteLine();
        }
    }

    public interface IGameSystem
    {
        void Initialize();
        void LoadContent();
        void Draw(GameTime gametime);
        
    }

    
}
