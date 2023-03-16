using CarService.Client.Core.MVVM;
using System.Drawing;

namespace CarService.Features.ShopInterface.Model
{
    public class Procedure : RestorableObject
    {
        private string name = string.Empty;
        private Color color;

        public int Id { get; set; }
        public IEnumerable<string> UsedByWarrantTypes { get; set; } = new List<string>();
        public string Name { get => name; set => SetProperty(ref name, value); }
        public Color ForegroundColor => GetForegroundColor();
        public Color Color
        {
            get => color;
            set
            {
                SetProperty(ref color, value);

                OnPropertyChanged(nameof(ForegroundColor));
            }
        }

        public Procedure() { }

        public Procedure(string name)
        {
            this.Name = name;

            Color = Color.FromArgb(Random.Shared.Next(255),
                Random.Shared.Next(255),
                Random.Shared.Next(255));
        }

        private Color GetForegroundColor()
        {
            int[] colors = new int[] { Color.R, Color.G, Color.B };

            return colors.Average() > 255 / 2 ? Color.Black : Color.White;
        }
    }
}