namespace SchoolManagementSystem
{
    public class PaginationMgm<T> where T : ISelectable
    {
        private List<T> allData;
        private readonly int pageSize = 15;
        private readonly Action<List<T>> onPageChanged;
        
        public int CurrentPage { get; private set; } = 1;
        public int TotalPages => (int)Math.Ceiling(allData.Count / (double)pageSize);

        public PaginationMgm(List<T> data,Action<List<T>> onPageChanged)
        {
            this.allData = data ?? new List<T>();
            this.onPageChanged = onPageChanged;

            
        }

        public void LoadPage(int page=1)
        {
            if (TotalPages == 0)
            {
                CurrentPage = 0;
                onPageChanged?.Invoke(new List<T>());
                return;
            }

            if (page < 1 || page > TotalPages) return;

            CurrentPage = page;
            var pageData = allData.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
            onPageChanged?.Invoke(pageData);
        }

        public void NextPage()
        {
            if (CurrentPage < TotalPages)
                LoadPage(CurrentPage + 1);
        }

        public void PreviousPage()
        {
            if (CurrentPage > 1)
                LoadPage(CurrentPage - 1);
        }
        //public void setData(List<T> data)
        //{
        //    allData = data;
        //}
        public List<T> AllData { get { return allData; } 
            set {
                allData = value;
                if(CurrentPage>0)LoadPage(CurrentPage);
                else LoadPage(1);
            }
        }
        public void SelectAll(bool isSelected)
        {
            foreach (var item in AllData)
                item.IsSelected = isSelected;
        }
        public List<T> GetSelectedItems()
        {
            return AllData.Where(x => x.IsSelected).ToList();
        }
        public void Refresh()
        {
            LoadPage(CurrentPage);
        }
    }

}
