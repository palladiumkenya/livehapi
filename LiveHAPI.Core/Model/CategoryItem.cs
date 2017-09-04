using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class CategoryItem : Entity<Guid>
    {
        private string _display;
        private bool _selected;
        private bool _allow = true;
        private bool _itemChecked;
        private decimal _rank;

        
        public Guid CategoryId { get; set; }
        
        public Guid ItemId { get; set; }
        
        public Item Item { get; set; }

        public string Display
        {
            get { return string.IsNullOrWhiteSpace(_display) ? Item?.Display : _display; }
            set { _display = value; }
        }

        public Decimal Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        
        public bool Selected
        {
            get { return _selected; }
            set
            {

                if (value != _selected)
                {
                    _selected = value;
                    NotifyOptionChanged(Id, _selected);
                }
            }
        }

        
        public bool ItemChecked
        {
            get { return _itemChecked; }
            set { _itemChecked = value; }
        }

        
        public bool Allow
        {
            get { return _allow; }
            set { _allow = value; }
        }

        public event EventHandler<OptionSelectedEventArgs> OptionSelected;

        public CategoryItem()
        {
            Id = LiveGuid.NewGuid();
        }

        private CategoryItem(Guid id, bool selected) 
        {
            Id = id;
            Selected = selected;
        }

        private CategoryItem(Guid id, Guid categoryId, Guid itemId, string display, decimal rank)
        {
            Id = id;
            _display = display;
            CategoryId = categoryId;
            ItemId = itemId;
            Rank = rank;
        }

        public static CategoryItem CreateInitial(string display)
        {
            return new CategoryItem(Guid.Empty, Guid.Empty, Guid.Empty,display,-1);
        }

        public static CategoryItem CreateForNotification(Guid id, bool selected)
        {
            return new CategoryItem(id,selected);
        }

        private void NotifyOptionChanged(Guid id,bool isSelected)
        {
            if (OptionSelected != null)
            {
                OptionSelected(this, new OptionSelectedEventArgs(id,isSelected));
            }
        }

        public override string ToString()
        {
            return Display;
        }
    }
}