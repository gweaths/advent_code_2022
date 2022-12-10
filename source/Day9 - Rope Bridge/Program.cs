string[] input = File.ReadAllLines("sample.txt");

void Part1()
{
    int rowH = 0;
    int rowTail = 0;

    int columnH = 0;
    int columnTail = 0;

    var positions = new HashSet<string>();
    positions.Add(position(rowTail, columnTail));

    foreach (var line in input)
    {
        var instruction = line.Split(" ");

        for (int i = 0; i < Convert.ToInt32(instruction[1]); i++)
        {
            int prevRowHead = rowH;
            int prevColumnHead = columnH;

            char movement = Convert.ToChar(instruction[0]);
            switch (movement)
            {
                case 'U':
                    columnH++;
                    break;
                case 'D':
                    columnH--;
                    break;
                case 'L':
                    rowH--;
                    break;
                case 'R':
                    rowH++;
                    break;
            }

            //Same row within 1 of each other, or same column 1 row between them
            if (rowH == rowTail && (columnH == columnTail || Math.Abs(columnH - columnTail) == 1)
                || columnH == columnTail && Math.Abs(rowH - rowTail) == 1)
            {
                continue;
            }

            //Already 1 space between them
            if (Math.Abs(columnH - columnTail) == 1 && Math.Abs(rowH - rowTail) == 1) continue;

            rowTail = prevRowHead;
            columnTail = prevColumnHead;

            positions.Add(position(rowTail, columnTail));
        }
    }

    Console.WriteLine(positions.Count);
}

void Part2()
{
    int rowHead = 0;
    var rowTails = new int[9];

    int columnHead = 0;
    var columnTails = new int[9];

    var positions = new HashSet<string>();
    positions.Add(position(rowHead, columnHead));

    foreach (var line in input)
    {
        var instruction = line.Split(" ");

        for (int i = 0; i < Convert.ToInt32(instruction[1]); i++)
        {
            int prevX = rowHead;
            int prevY = columnHead;

            switch (instruction[0])
            {
                case "U":
                    columnHead++;
                    break;
                case "D":
                    columnHead--;
                    break;
                case "L":
                    rowHead--;
                    break;
                case "R":
                    rowHead++;
                    break;
            }

            for (int tail = 0; tail < 9; tail++)
            {
                int xTail = rowTails[tail];
                int yTail = columnTails[tail];

                int xCompare = tail != 0 ? rowTails[tail - 1] : rowHead;
                int yCompare = tail != 0 ? columnTails[tail - 1] : columnHead;

                if (xCompare == xTail && (yCompare == yTail || Math.Abs(yCompare - yTail) == 1) || yCompare == yTail && Math.Abs(xCompare - xTail) == 1) continue;
                if (Math.Abs(yCompare - yTail) == 1 && Math.Abs(xCompare - xTail) == 1) continue;

                if (xCompare == xTail)
                {
                    columnTails[tail] += yCompare > yTail ? 1 : -1;
                }
                else if (yCompare == yTail)
                {
                    rowTails[tail] += xCompare > xTail ? 1 : -1;
                }
                else
                {
                    columnTails[tail] += yCompare > yTail ? 1 : -1;
                    rowTails[tail] += xCompare > xTail ? 1 : -1;
                }


                if (tail == 8)
                {
                    positions.Add(position(rowTails[tail], columnTails[tail]));
                }
            }
        }
    }

    Console.WriteLine(positions.Count);
}

string position(int x, int y)
{
    return $"{x.ToString()},{y.ToString()}";
}

Part1();
Part2();