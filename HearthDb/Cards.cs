﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using HearthDb.Enums;

#endregion

namespace HearthDb
{
	public static class Cards
	{
		public static readonly Dictionary<string, Card> All = new Dictionary<string, Card>();

		public static readonly Dictionary<string, Card> Collectible = new Dictionary<string, Card>();

		static Cards()
		{
			var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HearthDb.CardDefs.xml");
			if(stream == null)
				return;
			using(TextReader tr = new StreamReader(stream))
			{
				var xml = new XmlSerializer(typeof(CardDefs.CardDefs));
				var cardDefs = (CardDefs.CardDefs)xml.Deserialize(tr);
				foreach(var entity in cardDefs.Entites)
				{
					var card = new Card(entity);
					All.Add(entity.CardId, card);
					if(card.Collectible && card.Type != CardType.HERO)
						Collectible.Add(entity.CardId, card);
				}
			}
		}

		public static Card GetFromName(string name, Language lang, bool collectible = true)
			=> (collectible ? Collectible : All).Values.FirstOrDefault(x => x.GetLocName(lang).Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
}