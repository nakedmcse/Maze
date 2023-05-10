namespace Maze;

class Program
{
    static List<string> maze = new List<string>() {
        "######################",
        "#### ###   #   # ### #",
        "# #   ## # # #     #  ",
        "# # #    #   ##### # #",
        "# # # ########   # # #",
        "# # #      #   # # # #",
        "#   # ## #   #   #   #",
        "######################"
    };

    static List<string> explored = new List<string>() {
        "                      ",
        "                      ",
        "                      ",
        "                      ",
        "                      ",
        "                      ",
        "                      ",
        "                      ",
    };

    static int x,y = 0;

    static string threeBlock(string a) {
        return a+a+a;
    }

    static void displayMaze() {
        Console.WriteLine("###" + threeBlock(maze[x-1][y].ToString()) + "###");
        Console.WriteLine("###" + threeBlock(maze[x-1][y].ToString()) + "###");
        Console.WriteLine(threeBlock(maze[x][y-1].ToString()) + " o " + threeBlock(maze[x][y+1].ToString()));
        Console.WriteLine("###" + threeBlock(maze[x+1][y].ToString()) + "###");
        Console.WriteLine("###" + threeBlock(maze[x+1][y].ToString()) + "###");
    }

    static void displayHelp() {
        Console.Clear();
        var origLine = explored[x];
        explored[x] = explored[x].Substring(0,y) + 'o' + explored[x].Substring(y+1);
        foreach(var line in explored) {
            Console.WriteLine(line);
        }
        explored[x] = origLine;
        Console.WriteLine("Press Enter to Continue");
        Console.ReadKey();
        Console.Clear();
    }

    static void updateExplored() {
        //Extract 3x3 square from maze around x,y
        var topThree = maze[x-1].Substring(y-1,3);
        var midThree = maze[x].Substring(y-1,3);
        var bottomThree = maze[x+1].Substring(y-1,3);
        //Insert 3x3 square into explored
        explored[x-1] = explored[x-1].Substring(0, y-1) + topThree + explored[x-1].Substring(y+2);
        explored[x] = explored[x].Substring(0, y-1) + midThree + explored[x].Substring(y+2);
        explored[x+1] = explored[x+1].Substring(0, y-1) + bottomThree + explored[x+1].Substring(y+2);
    }

    static void keyAction(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.W:
                if(maze[x-1][y]=='#') break;
                x -= 1;
                break;
            case ConsoleKey.S:
                if(maze[x+1][y]=='#') break;
                x += 1;
                break;
            case ConsoleKey.A:
                if(maze[x][y-1]=='#') break;
                y -= 1;
                break;
            case ConsoleKey.D:
                if(maze[x][y+1]=='#') break;
                y += 1;
                break;
            case ConsoleKey.H:
                displayHelp();
                break;
            default:
                break;
        }
    }

    static void Main(string[] args)
    {
        //Set start coords
        x = 2;
        y = 1;

        //Game loop
        while(true) {
            displayMaze();
            updateExplored();
            var keyInfo = Console.ReadKey();
            Console.Clear();
            keyAction(keyInfo);
            if(y == maze[0].Length - 1) break;  //Check for winning condition
        }

        //Won
        Console.WriteLine("You won");
    }
}