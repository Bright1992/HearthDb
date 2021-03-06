#region

using System;
using System.Linq;
using HearthDb.CardDefs;
using HearthDb.Enums;
using static HearthDb.Enums.GameTag;

#endregion

namespace HearthDb
{
	public class Card
	{
		internal Card(Entity entity)
		{
			Entity = entity;
		}

		public Entity Entity { get; }

		public string Id => Entity.CardId;

		public string Name => GetLocName(DefaultLanguage);

		public string Text => GetLocText(DefaultLanguage);

		public string FlavorText => GetLocFlavorText(DefaultLanguage);

		public CardClass Class => (CardClass)Entity.GetTag(CLASS);

		public Rarity Rarity => (Rarity)Entity.GetTag(RARITY);

		public CardType Type => (CardType)Entity.GetTag(CARDTYPE);

		public Race Race => (Race)Entity.GetTag(CARDRACE);

		public CardSet Set => (CardSet)Entity.GetTag(CARD_SET);

		public Faction Faction => (Faction)Entity.GetTag(FACTION);

		public int Cost => Entity.GetTag(COST);

		public int Attack => Entity.GetTag(ATK);

		public int Health => Entity.GetTag(HEALTH);

		public int Durability => Entity.GetTag(DURABILITY);

		public string[] Mechanics
		{
			get
			{
				var mechanics = Dictionaries.Mechanics.Keys.Where(mechanic => Entity.GetTag(mechanic) > 0).Select(x => Dictionaries.Mechanics[x]);
				var refMechanics =
					Dictionaries.ReferencedMechanics.Keys.Where(mechanic => Entity.GetTag(mechanic) > 0)
								.Select(x => Dictionaries.ReferencedMechanics[x]);
				return mechanics.Concat(refMechanics).ToArray();
			}
		}

		public string ArtistName => Entity.GetInnerValue(ARTISTNAME);

		public string[] EntourageCardIds => Entity.EntourageCards.Select(x => x.CardId).ToArray();

		public Language DefaultLanguage { get; set; } = Language.enUS;

		public bool Collectible => Convert.ToBoolean(Entity.GetTag(GameTag.Collectible));

		public string GetLocName(Language lang) => Entity.GetLocString(CARDNAME, lang);

		public string GetLocText(Language lang) => Entity.GetLocString(CARDTEXT_INHAND, lang)?.Trim();

		public string GetLocFlavorText(Language lang) => Entity.GetLocString(FLAVORTEXT, lang);
	}
}