using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;
using PathFinder.Scene;
using SharpDX.Toolkit;

namespace TotL.Labyrinth.Scene
{
    public class LabyrinthTerrain : ProcTerrain
    {
        public MapBuilder mapBuilder;
        public LabyrinthTerrain(TerrainTile[,] terrain, Connection[,] connection) : base(terrain, connection)
        {
            mapBuilder = new LabyrinthBuilder(connection, terrain);
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
           mapBuilder = new LabyrinthBuilder(Connect, Map);

            mapBuilder.Build();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
