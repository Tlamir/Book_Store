using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*\class Magazine
* \brief Product Sınıfından türetilmiştir. Magaazine iiçin gerekli bilgilerin tutulmasını sağlar.
*
*/
namespace OnlineBookStore
{
    public enum MagazineType
    {
        Gaming,
        History,
        News,
        Computer,
        Geography,
        Actual,
    }

    public class Magazine : Product
    {
        private string issue;
        private MagazineType magazineType;

        public string Issue
        {
            get
            {
                return issue;
            }
            set
            {
                issue = value;
            }
        }

        internal MagazineType MagazineType
        {
            get
            {
                return magazineType;
            }
            set
            {
                magazineType = value;
            }
        }
        public Magazine(string id, string name, double price) : base(id, name, price) { }

        public override void ShowDetails()
        {
            /*MagazineForm magazineForm = new MagazineForm(this);
            magazineForm.ShowDialog();                                later we ll implement this
            magazineForm.Dispose();*/
        }

        public static MagazineType GetMagazineType(string Mtype)
        {
            MagazineType magazineType = new MagazineType();
            switch (Mtype)
            {
                case "Gaming":
                    magazineType = MagazineType.Gaming;
                    break;
                case "History":
                    magazineType = MagazineType.History;
                    break;
                case "News":
                    magazineType = MagazineType.News;
                    break;
                case "Computer":
                    magazineType = MagazineType.Computer;
                    break;
                case "Geography":
                    magazineType = MagazineType.Geography;
                    break;
                case "Actual":
                    magazineType = MagazineType.Actual;
                    break;
            }
            return magazineType;
        }
    }
}
