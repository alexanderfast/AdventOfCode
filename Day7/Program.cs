using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day7
{
	internal class InputRepository
	{
		private readonly Dictionary<string, Wire> m_wires = new Dictionary<string, Wire>();
		private readonly List<IInput> m_inputs = new List<IInput>(); 

		public Wire Get(string id)
		{
			return m_wires[id];
		}

		public void Add(IInput input)
		{
			m_inputs.Add(input);
			var wire = input as Wire;

			// wires can be accessed by name
			if (wire != null)
				m_wires.Add(wire.Id, wire);
		}

		public IEnumerable<IInput> GetAll()
		{
			return m_inputs;
		}

		public void Delete(string id)
		{
			m_wires.Remove(id);
			for (int i = 0; i < m_inputs.Count; i++)
			{
				var wire = m_inputs[i] as Wire;
				if (wire != null)
				{
					if (wire.Id == id);
					{
						m_inputs.RemoveAt(i);
						break;
					}
				}
			}
		}
	}

	internal interface IInput
	{
		InputRepository Inputs { get; set; }
		ushort GetValue();
		void Reset();
	}

	internal abstract class InputBase : IInput
	{
		private ushort? m_inputCached;

		public InputRepository Inputs { get; set; }

		public ushort GetValue()
		{
			if (!m_inputCached.HasValue)
			{
				m_inputCached = GetValueImpl();
			}
			return m_inputCached.Value;
		}

		public void Reset()
		{
			m_inputCached = null;
		}

		protected ushort Evaluate(string id)
		{
			ushort parsed;
			if (ushort.TryParse(id, out parsed))
				return parsed; // constant
			return Inputs.Get(id).GetValue();
		}

		protected abstract ushort GetValueImpl();
	}

	internal class Wire : InputBase
	{
		private readonly IInput m_iinput;

		public Wire(string id, IInput iinput)
		{
			m_iinput = iinput;
			Id = id;
		}

		public string Id { get; private set; }

		protected override ushort GetValueImpl()
		{
			return m_iinput.GetValue();
		}

		public override string ToString()
		{
			return Id;
		}
	}

	internal class Constant : InputBase
	{
		private readonly string m_input;

		public Constant(string input)
		{
			m_input = input;
		}

		protected override ushort GetValueImpl()
		{
			return Evaluate(m_input);
		}
	}

	internal class And : InputBase
	{
		private readonly string m_inputA;
		private readonly string m_inputB;

		public And(string inputA, string inputB)
		{
			m_inputA = inputA;
			m_inputB = inputB;
		}

		protected override ushort GetValueImpl()
		{
			var a = Evaluate(m_inputA);
			var b = Evaluate(m_inputB);
			return (ushort)(a & b);
		}
	}

	internal class Or : InputBase
	{
		private readonly string m_inputA;
		private readonly string m_inputB;

		public Or(string inputA, string inputB)
		{
			m_inputA = inputA;
			m_inputB = inputB;
		}

		protected override ushort GetValueImpl()
		{
			var a = Evaluate(m_inputA);
			var b = Evaluate(m_inputB);
			return (ushort)(a | b);
		}
	}

	internal class Lshift : InputBase
	{
		private readonly int m_amount;
		private readonly string m_inputA;

		public Lshift(string inputA, int amount)
		{
			m_inputA = inputA;
			m_amount = amount;
		}

		protected override ushort GetValueImpl()
		{
			var a = Evaluate(m_inputA);
			return (ushort)(a << m_amount);
		}
	}

	internal class Rshift : InputBase
	{
		private readonly int m_amount;
		private readonly string m_inputA;

		public Rshift(string inputA, int amount)
		{
			m_inputA = inputA;
			m_amount = amount;
		}

		protected override ushort GetValueImpl()
		{
			var a = Evaluate(m_inputA);
			return (ushort)(a >> m_amount);
		}
	}

	internal class Not : InputBase
	{
		private readonly string m_inputA;

		public Not(string inputA)
		{
			m_inputA = inputA;
		}

		protected override ushort GetValueImpl()
		{
			var a = Evaluate(m_inputA);
			return (ushort)~a;
		}
	}

	internal class Parser
	{
		private readonly InputRepository m_repos;

		public Parser(InputRepository repos)
		{
			m_repos = repos;
		}

		public void Parse(IEnumerable<string> lines)
		{
			foreach (string line in lines)
			{
				string[] parts = Split(line, " -> ").ToArray();
				string input = parts[0];
				IInput iinput;
				if (input.Contains("AND"))
				{
					string[] ab = Split(input, "AND");
					iinput = new And(ab[0], ab[1]);
				}
				else if (input.Contains("OR"))
				{
					string[] ab = Split(input, "OR");
					iinput = new Or(ab[0], ab[1]);
				}
				else if (input.Contains("LSHIFT"))
				{
					string[] ab = Split(input, "LSHIFT");
					iinput = new Lshift(ab[0], int.Parse(ab[1]));
				}
				else if (input.Contains("RSHIFT"))
				{
					string[] ab = Split(input, "RSHIFT");
					iinput = new Rshift(ab[0], int.Parse(ab[1]));
				}
				else if (input.Contains("NOT"))
				{
					string[] ab = Split(input, "NOT");
					iinput = new Not(ab[0]);
				}
				else
				{
					iinput = new Constant(input);
				}
				iinput.Inputs = m_repos;
				m_repos.Add(iinput);

				string id = parts[1].Trim();
				int parsedId;
				if (int.TryParse(id, out parsedId))
					throw new InvalidOperationException(id);

				var wire = new Wire(id, iinput);
				m_repos.Add(wire);
			}
		}

		private string[] Split(string s, string delimiter)
		{
			return s.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
		}
	}

	internal class Program
	{
		private static void Main(string[] args)
		{
			var repos = new InputRepository();

			//new Parser(repos).Parse(File.ReadAllLines("Test1.txt"));
			//foreach (var key in "defghixy")
			//{
			//	var wire = repos.Get(key.ToString());
			//	Debug.WriteLine("{0}: {1}", wire.Id, wire.GetValue());
			//}

			new Parser(repos).Parse(File.ReadAllLines("Input.txt"));
			var value = repos.Get("a").GetValue();
			Debug.WriteLine(value);

			// part 2
			// reset
			foreach (var input in repos.GetAll())
				input.Reset();
			// find b, overide to constant value
			repos.Delete("b");
			var wire = new Wire("b", new Constant("3176") { Inputs = repos });
			repos.Add(wire);
			value = repos.Get("a").GetValue();
			Debug.WriteLine(value);
		}
	}
}
