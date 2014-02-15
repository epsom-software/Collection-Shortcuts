

namespace CollectionShortcuts.Tests.AnnoyingPairs.NamespaceA
{
	
	

	public class TypeValuePair : IPair
	{
		public string Type { get; set; }
		public string Value { get; set; }

		public string Key
		{
			get { return Type; }
			set { Type = value; }
		}
	}
		

	public class NameValuePair : IPair
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public string Key
		{
			get { return Name; }
			set { Name = value; }
		}
	}
		

}


namespace CollectionShortcuts.Tests.AnnoyingPairs.NamespaceB
{
	
	

	public class TypeValuePair : IPair
	{
		public string Type { get; set; }
		public string Value { get; set; }

		public string Key
		{
			get { return Type; }
			set { Type = value; }
		}
	}
		

	public class NameValuePair : IPair
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public string Key
		{
			get { return Name; }
			set { Name = value; }
		}
	}
		

}

