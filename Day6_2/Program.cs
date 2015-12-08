using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day6_2
{
	class Grid<T>
	{
		private readonly T[] m_data;

		public int SizeX { get; private set; }
		public int SizeY { get; private set; }

		public Grid(int sizeX, int sizeY, T defaultValue)
		{
			SizeX = sizeX;
			SizeY = sizeY;
			m_data = Enumerable.Repeat(defaultValue, SizeX * SizeY).ToArray();
		}

		public T Get(int x, int y)
		{
			return m_data[(SizeX * y) + x];
		}

		public void Set(int x, int y, T data)
		{
			m_data[(SizeX * y) + x] = data;
		}

		public IEnumerable<T> GetAll()
		{
			return m_data;
		}
	}

	class Command
	{
		public enum Type
		{
			TurnOn,
			TurnOff,
			Toggle
		}

		public Type CommandType { get; set; }
		public int X1 { get; set; }
		public int Y1 { get; set; }
		public int X2 { get; set; }
		public int Y2 { get; set; }
	}

	class Parser
	{
		private int m_index;
		private string m_current;
		private string[] m_data;

		public IEnumerable<Command> Parse(string input)
		{
			m_data = input.Split();;
			for (m_index = 0, m_current = m_data[m_index]; m_index < m_data.Length; Next())
			{
				if (string.IsNullOrEmpty(m_current))
					continue;
				var command = new Command();
				if (m_current == "turn")
				{
					Next();
					if (m_current == "on")
						command.CommandType = Command.Type.TurnOn;
					else if (m_current == "off")
						command.CommandType = Command.Type.TurnOff;
					else
						throw new ArgumentException(m_current);
				}
				else if (m_current == "toggle")
				{
					command.CommandType = Command.Type.Toggle;
				}
				else
				{
					throw new ArgumentException(m_current);
				}

				Next();
				var xy = m_current.Split(',');
				command.X1 = int.Parse(xy[0]);
				command.Y1 = int.Parse(xy[1]);

				Next();
				if (m_current != "through")
					throw new ArgumentException(m_current);

				Next();
				xy = m_current.Split(',');
				command.X2 = int.Parse(xy[0]);
				command.Y2 = int.Parse(xy[1]);

				yield return command;
			}
		}

		private void Next()
		{
			do
			{
				++m_index;
				if (m_index >= m_data.Length)
					return;
				m_current = m_data[m_index];				
			} while (string.IsNullOrEmpty(m_current));
		}
	}

	class CommandHandler
	{
		private readonly Grid<bool> m_grid;

		public CommandHandler(Grid<bool> grid)
		{
			m_grid = grid;
		}

		public void Handle(IEnumerable<Command> commands)
		{
			foreach (var command in commands)
				Handle(command);
		}

		public void Handle(Command command)
		{
			if (command.CommandType == Command.Type.TurnOn)
			{
				SetArea(command.X1, command.Y1, command.X2, command.Y2, true);
			}
			else if (command.CommandType == Command.Type.TurnOff)
			{
				SetArea(command.X1, command.Y1, command.X2, command.Y2, false);
			}
			else if (command.CommandType == Command.Type.Toggle)
			{
				ToggleArea(command.X1, command.Y1, command.X2, command.Y2);
			}
		}

		private void SetArea(int x1, int y1, int x2, int y2, bool data)
		{
			for (int y = y1; y <= y2; y++)
			{
				for (int x = x1; x <= x2; x++)
				{
					m_grid.Set(x, y, data);
				}
			}
		}

		private void ToggleArea(int x1, int y1, int x2, int y2)
		{
			for (int y = y1; y <= y2; y++)
			{
				for (int x = x1; x <= x2; x++)
				{
					var current = m_grid.Get(x, y);
					m_grid.Set(x, y, !current);
				}
			}
		}
	}

	class CommandHandler2
	{
		private readonly Grid<int> m_grid;

		public CommandHandler2(Grid<int> grid)
		{
			m_grid = grid;
		}

		public void Handle(IEnumerable<Command> commands)
		{
			foreach (var command in commands)
				Handle(command);
		}

		public void Handle(Command command)
		{
			if (command.CommandType == Command.Type.TurnOn)
			{
				AlterBrightness(command.X1, command.Y1, command.X2, command.Y2, 1);
			}
			else if (command.CommandType == Command.Type.TurnOff)
			{
				AlterBrightness(command.X1, command.Y1, command.X2, command.Y2, -1);
			}
			else if (command.CommandType == Command.Type.Toggle)
			{
				AlterBrightness(command.X1, command.Y1, command.X2, command.Y2, 2);
			}
		}

		private void AlterBrightness(int x1, int y1, int x2, int y2, int amount)
		{
			for (int y = y1; y <= y2; y++)
			{
				for (int x = x1; x <= x2; x++)
				{
					var current = m_grid.Get(x, y) + amount;
					current = Math.Max(current, 0);
					m_grid.Set(x, y, current);
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Part2();
		}

		private static void Part1()
		{
			//var grid = new Grid(2, 2);
			//new CommandHandler(grid).Handle(new Parser().Parse("turn on 0,0 through 1,1"));

			var grid = Run1(Input.Test1);
			Debug.Assert(grid.GetAll().Count(x => x) == 1000 * 1000);

			grid = Run1(Input.Test2);
			Debug.Assert(grid.GetAll().Count(x => x) == 1000);

			grid = Run1(Input.Test3);
			Debug.Assert(grid.GetAll().Count(x => x) == 0);

			grid = Run1(Input.Data);
			var count = grid.GetAll().Count(x => x);
			Console.WriteLine(count);
		}

		private static void Part2()
		{
			//var grid = Run2(Input.Part2Test1);
			//Debug.Assert(grid.GetAll().Sum() == 1);

			//grid = Run2(Input.Part2Test2);
			//Debug.Assert(grid.GetAll().Sum() == 2000000);

			var grid = Run2(Input.Data);
			var count = grid.GetAll().Sum();
			Console.WriteLine(count);
		}

		private static Grid<bool> Run1(string input, int sizeX = 1000, int sizeY = 1000)
		{
			var grid = new Grid<bool>(sizeX, sizeY, false);
			var handler = new CommandHandler(grid);
			var parser = new Parser();
			handler.Handle(parser.Parse(input));
			return grid;
		}

		private static Grid<int> Run2(string input, int sizeX = 1000, int sizeY = 1000)
		{
			var grid = new Grid<int>(sizeX, sizeY, 0);
			var handler = new CommandHandler2(grid);
			var parser = new Parser();
			handler.Handle(parser.Parse(input));
			return grid;
		}
	}
}
