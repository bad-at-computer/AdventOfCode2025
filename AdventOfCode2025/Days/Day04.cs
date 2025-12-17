namespace AdventOfCode2025.Days;

public class Day04 : IDay
{
    public int DayNumber => 4;

    public string SolvePart1(string input)
    {
        input =
            "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
        
        var accessibleRolls = 0;
        var rows = input.Split('\n');
        var gridWidth = rows[0].Length;
        var gridHeight = rows.Length;
      
        var paperGrid = new char[gridWidth, gridHeight]; // accessed using [col][row] / [x,y]

        // Populate the grid
        for (var row = 0; row < gridWidth; row++)
        {
            for (var col = 0; col < gridHeight; col++)
            {
                paperGrid[col, row] = rows[row][col];
            }
        }
        
        // Iterate over the grid
        for (var row = 0; row < gridWidth; row++)
        {
            for (var col = 0; col < gridHeight; col++)
            {
                if (!paperGrid[col, row].Equals('@')) continue;
                // Console.WriteLine($"Looking at [{col},{row}]");

                var adjacentRolls = LookAround(paperGrid, col, row);
                
                if (adjacentRolls < 4)
                {
                    accessibleRolls++;
                    // Console.WriteLine($"[{col},{row}] is accessible");
                }
            }
        }
        return accessibleRolls.ToString();
    }

    int LookAround(char[,] paperGrid, int col, int row)
    {
        var adjacentRolls = 0;
        
        adjacentRolls += LookHere(paperGrid, col - 1, row - 1); // top left
        adjacentRolls += LookHere(paperGrid, col, row - 1);         // top
        adjacentRolls += LookHere(paperGrid, col + 1, row - 1); // top right
        adjacentRolls += LookHere(paperGrid, col + 1, row);         // right
        adjacentRolls += LookHere(paperGrid, col + 1, row + 1); // bottom right
        adjacentRolls += LookHere(paperGrid, col, row + 1);         // bottom
        adjacentRolls += LookHere(paperGrid, col - 1, row + 1); // bottom left
        adjacentRolls += LookHere(paperGrid, col - 1, row);         // left
        
        return adjacentRolls;
    }

    int LookHere(char[,] paperGrid, int col, int row)
    {
        try
        {
            return paperGrid[col, row].Equals('@') ? 1 : 0;
        }
        catch (IndexOutOfRangeException ex)
        {
            return 0;
        }
    }

    public string SolvePart2(string input)
    {
        // input =
        // "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
        
        var accessibleRolls = 0;
        var rows = input.Split('\n');
        var gridWidth = rows[0].Length;
        var gridHeight = rows.Length;
      
        var paperGrid = new char[gridWidth, gridHeight]; // accessed using [col][row] / [x,y]

        // Populate the grid
        for (var row = 0; row < gridWidth; row++)
        {
            for (var col = 0; col < gridHeight; col++)
            {
                paperGrid[col, row] = rows[row][col];
            }
        }
        
        // Iterate over the grid repeatedly, removing accessible rolls in each until 0 remain
        while (true)
        {
            var foundRolls = IterateGrid(paperGrid);
            accessibleRolls += foundRolls.Count;
            
            // Console.WriteLine($"Found {foundRolls.Count} accessible rolls");
            if (foundRolls.Count <= 0) break;
            paperGrid = RemoveRolls(paperGrid, foundRolls);
        }

        
        return accessibleRolls.ToString();
    }
    
    private List<Coordinate> IterateGrid(char[,] paperGrid)
    {
        var foundRolls = new List<Coordinate>();
        for (var row = 0; row < paperGrid.GetLength(0); row++)
        {
            for (var col = 0; col < paperGrid.GetLength(1); col++)
            {
                if (!paperGrid[col, row].Equals('@')) continue;
                // Console.WriteLine($"Looking at [{col},{row}]");

                var adjacentRolls = LookAround(paperGrid, col, row);
                    
                if (adjacentRolls < 4)
                {
                    foundRolls.Add(new Coordinate(col, row));
                    // Console.WriteLine($"[{col},{row}] is accessible");
                }
            }
        }

        return foundRolls;
    }

    char[,] RemoveRolls(char[,] paperGrid, List<Coordinate> foundRolls)
    {
        foreach (var coordinate in foundRolls)
        {
            paperGrid[coordinate.Col, coordinate.Row] = '.';
        }

        return paperGrid;
    }

    class Coordinate
    {
        public Coordinate(int col, int row)
        {
            Row = row;
            Col = col;
        }
        public int Row { get; set; } = 0;
        public int Col { get; set; } = 0;
    }
}