using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*\class MusicType
* \brief Yine Product sınıfından Factory Tasarım kalıbına uygun olarak türetilmiştir. 
*
*/

namespace OnlineBookStore
{
    public enum MusicType
    {
        HipHop,
        Metal,
        Rock,
        Jazzrapbreakbeat,
        Classic,
    }

    public class MusicCD : Product
    {
        private int releaseDate;
        private string singer;
        private string content;
        private MusicType cdType;


        public string Singer
        {
            get
            {
                return singer;
            }

            set
            {
                singer = value;
            }
        }

        public int ReleaseDate
        {
            get
            {
                return releaseDate;
            }

            set
            {
                releaseDate = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        internal MusicType CdType
        {
            get
            {
                return cdType;
            }

            set
            {
                cdType = value;
            }
        }

        public MusicCD(string id, string name, double price) : base(id, name, price) { }

        public override void ShowDetails()
        {
           /* MusicForm musicForm = new MusicForm(this);
            musicForm.ShowDialog();
            musicForm.Dispose();*/
        }

        public static MusicType GetMusicType(string Mtype)
        {
            MusicType musicType = new MusicType();
            switch (Mtype)
            {
                case "HipHop":
                    musicType = MusicType.HipHop;
                    break;
                case "Metal":
                    musicType = MusicType.Metal;
                    break;
                case "Rock":
                    musicType = MusicType.Rock;
                    break;
                case "Jazzrapbreakbeat":
                    musicType = MusicType.Jazzrapbreakbeat;
                    break;
                case "Classic":
                    musicType = MusicType.Classic;
                    break;            
            }
            return musicType;
        }
    }
}
