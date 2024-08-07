﻿using _0_Framework.Domain;

namespace ShopManagement.Domain.SliderAgg
{
	public class Slide : EntityBase
	{
		public string Picture { get; private set; }
		public string PictureAlt { get; private set; }
		public string PictureTitle { get; private set; }
		public string Heading { get; private set; }
		public string Title { get; private set; }
		public string Text { get; private set; }
		public string BtnText { get; private set; }
		public string Link { get; private set; }
		public bool IsDeleted { get; private set; }

		public Slide(string picture, string pictureAlt, string pictureTitle, string heading,
			string title, string text, string btnText, string link)
		{
			Picture = picture;
			PictureAlt = pictureAlt;
			PictureTitle = pictureTitle;
			Heading = heading;
			Title = title;
			Text = text;
			BtnText = btnText;
			IsDeleted = false;
			Link = link;
		}
		public void Edit(string picture, string pictureAlt, string pictureTitle, string heading,
			string title, string text, string btnText, string link)
		{
			if (!string.IsNullOrWhiteSpace(picture))
				Picture = picture;
			PictureAlt = pictureAlt;
			PictureTitle = pictureTitle;
			Heading = heading;
			Title = title;
			Text = text;
			BtnText = btnText;
			Link = link;
		}
		public void Delete()
		{
			IsDeleted = true;
		}
		public void Restore()
		{
			IsDeleted = false;
		}
	}
}
