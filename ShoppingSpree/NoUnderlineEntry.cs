namespace ShoppingSpree
{
    public class NoUnderlineEntry : Entry
    {
        public static readonly BindableProperty NoUnderlineProperty = BindableProperty.Create(
            nameof(NoUnderline),
            typeof(bool),
            typeof(NoUnderlineEntry),
            false
        );


        public bool NoUnderline
        {
            get => (bool)GetValue(NoUnderlineProperty);
            set => SetValue(NoUnderlineProperty, value);
        }

        public static readonly BindableProperty EntryBackgroundColorProperty =
             BindableProperty.Create(
                 nameof(EntryBackgroundColor),
                 typeof(Color),
                 typeof(NoUnderlineEntry),
                 Color.Parse("#f6ffff")
             );

        public Color EntryBackgroundColor
        {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }
    }
}