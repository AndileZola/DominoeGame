using System;

namespace DominoeGame
{
    class Program
    {
        static Player playerOne = new Player();
        static Player playerTwo = new Player();
        static Tile previousPlayedTile = new Tile();
        static string board = "";
        static Tile[] Stack = new Tile[]
        {
            new Tile{Mark="<0:0>",isUsed=false },new Tile{Mark="<0:1>",isUsed=false },
            new Tile{Mark="<0:2>",isUsed=false },new Tile{Mark="<0:3>",isUsed=false },
            new Tile{Mark="<0:4>",isUsed=false },new Tile{Mark="<0:5>",isUsed=false },
            new Tile{Mark="<0:6>",isUsed=false },new Tile{Mark="<1:1>",isUsed=false },
            new Tile{Mark="<1:2>",isUsed=false },new Tile{Mark="<1:3>",isUsed=false },
            new Tile{Mark="<1:4>",isUsed=false },new Tile{Mark="<1:5>",isUsed=false },
            new Tile{Mark="<1:6>",isUsed=false },new Tile{Mark="<2:2>",isUsed=false },
            new Tile{Mark="<2:3>",isUsed=false },new Tile{Mark="<2:4>",isUsed=false },
            new Tile{Mark="<2:5>",isUsed=false },new Tile{Mark="<2:6>",isUsed=false },
            new Tile{Mark="<3:3>",isUsed=false },new Tile{Mark="<3:4>",isUsed=false },
            new Tile{Mark="<3:5>",isUsed=false },new Tile{Mark="<3:6>",isUsed=false },
            new Tile{Mark="<4:4>",isUsed=false },new Tile{Mark="<4:5>",isUsed=false },
            new Tile{Mark="<4:6>",isUsed=false },new Tile{Mark="<5:5>",isUsed=false },
            new Tile{Mark="<5:6>",isUsed=false },new Tile{Mark="<6:6>",isUsed=false }
        };

        static void Main(string[] args)
        {
            //Assign Id's to the tiles
            AssignTileId();

            //First player draw 7 tiles
            playerOne = SetPlayer(1, 7);

            //Second player draw 7 tiles
            playerTwo = SetPlayer(2, 7);

            //Draw the first random tile to start the game
            previousPlayedTile = DrawSingleRandomTile(Stack,false); 

            Display($"Game starting with first tile: {previousPlayedTile.Mark}");

            //Start the game
            Play(1);

            Console.ReadKey();
        }

        //Instantiate the players
        static Player SetPlayer(int playerid,int tileCount)
        {
            Tile[] playerTiles = DrawMultipleRandomTiles(tileCount); //Draw 7 tiles for each player
            string playerName = (playerid == 1) ? "Alice" : "Bob";   //Get the name of the player       
            return new Player { PlayerId = playerid, Name = playerName, Tiles = playerTiles };
        }

        //Displays the progress of the game
        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        //Fakes the Id's of the tiles
        public static void AssignTileId()
        {
            for (int x = 0; x < 28; x++)
                Stack[x].TileId = x;
        }

        //Plays the game untill the end
        public static void Play(int playerId)
        {
            string message = "";
            for (int x = 0; x <= 28; x++)
            {
                Player player = (playerId == 1) ? playerOne : playerTwo;
                Tile tilePlayed = DrawSingleRandomTile(player.Tiles,true); //Draw single tile from player's tiles
                message = $"{player.Name} plays {tilePlayed.Mark} to connect to {previousPlayedTile.Mark} on the board";
                Display(message);
                board += $"{tilePlayed.Mark} {previousPlayedTile.Mark}";
                Display("Board is now:" + board);
                previousPlayedTile = tilePlayed;
            }
        }

        //Draws multiple tiles at a time
        static Tile[] DrawMultipleRandomTiles(int count)
        {
            Tile[] drawnTiles = new Tile[count];
            for (int x = 0; x < count; x++)
            {
                Tile tile = DrawSingleRandomTile(Stack,false);              //Draw from the stack

                drawnTiles[x] = tile;
            }
            return drawnTiles;
        }

        //Draws a single tile at a time
        static Tile DrawSingleRandomTile(Tile[] tiles,bool isOwnTile)
        {
            Random random = new Random();
            int tileIndex = random.Next(1, tiles.Length);
            Tile tile = tiles[tileIndex];
            bool isTileUsed = (tile.isUsed && !isOwnTile);

            if (isTileUsed)                                                 //If the tile is used:
            return DrawSingleRandomTile(tiles, isOwnTile);                  //Draw another one using recursion
            else
            {
                Stack[tileIndex].isUsed = true;                             //Mark tile as used, so that only one player can draw it
                Stack[tileIndex].TileId = tileIndex;
            }
            return tile;                                                    //Return the drawn tile
        }        

        //Compare if tiles match
        static bool isMatch(Tile[] tiles,int tileId)
        {
            for(int x = 0; x < tiles.Length; x++)
            {
                if (tiles[x].TileId == tileId)
                    return true;
            }
            return false;
        }

        class Tile {
            public int TileId { get; set; }
            public string Mark { get; set; }
            public bool isUsed { get; set; }
        }
        class Player
        {
            public int PlayerId { get; set; }
            public string Name { get; set; }
            public Tile[] Tiles{ get; set; }
            public int TileCount { get; set; }
        }
    }
}
