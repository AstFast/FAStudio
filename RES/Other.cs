namespace FAStudio.RES
{
	public class TypeCreate
	{
		public static int TypeReview(string type)
		{
			if (type == @"Microsoft.Xna.Framework.Content.SpriteFontReader, Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.Texture2DReader, Microsoft.Xna.Framework.Graphics, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = 842cf8be1de50553")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Rectangle, Microsoft.Xna.Framework, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = 842cf8be1de50553]]")
			{
				return 1;
			}
			else if (type == "Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553]]")
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
	}
	public struct Hander
	{
		public char target;
		public int formatVersion;
		public bool hidef;
		public bool compressed;
	}
	public struct Readers
	{
		public string type;
		public int verson;
	}
	public struct Texture
	{
		public int format;
		public string export;
	}
	public struct Glyphs
	{
		public int x;
		public int y;
		public int width;
		public int height;
	}
	public struct Cropping
	{
		public int x;
		public int y;
		public int width;
		public int height;
	}
	public struct Kerning
	{
		public Single x;
		public Single y;
		public Single z;
	}
	public struct Content
	{
		public Texture texture;
		public Glyphs[] glyphs;
		public Cropping[] cropping;
		public char[] characterMap;
		public int verticalLineSpacing;
		public Single horizontalSpacing;
		public Kerning[] kerning;
		public string DefaultCharacter;
	}
	public class Font_C
	{
		public Hander hander;
		public Readers[] readers;
		public Content content;
	}

	public class Font_JSON
	{

		public Hander hander { get; set; }
		public Readers[] readers { get; set; }
		public Content content { get; set; }

	}
}
