using System;
using VkNet.Model;

namespace ApiWrapper.Core
{
	public class PersonModel
	{
		internal PersonModel(User user)
		{
			this.id = user.Id;

			this.birthDate = user.BirthDate;

			this.city = user.City == null ? "" : user.City.Title;

			this.name = user.FirstName + " " + user.LastName;

			this.photoUrl400 = user.Photo400Orig;
			this.photoUrl200 = user.Photo200Orig;
			this.photoUrlMax = user.PhotoMaxOrig;
		    this.Status = user.Status;
		    this.interests = user.Interests;
		    this.followers = user.FollowersCount == null ? 0 : (int)user.FollowersCount;
		    this.Domain = user.Domain;

		}

		public PersonModel(int userVkId) : this(SearchInstrument.GetUser(userVkId))
		{
		}

		public string birthDate { get; set; }

		public Uri photoUrlMax { get; set; }

		public Uri photoUrl200 { get; set; }

		public Uri photoUrl400 { get; set; }

        public string Status { get; set; }
        public string interests { get; set; }

	    public int followers { get; set; }

        public string Domain { get; set; }

        public long id { get; set; }

		public string name { get; set; }

		public string city { get; set; }
	}
}