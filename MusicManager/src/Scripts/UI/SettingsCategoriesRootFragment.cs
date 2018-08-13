using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MusicManager
{
    public class SettingsCategoriesRootFragment : Fragment
    {
        View currentView;
        LinearLayout categoriesRoot;
        Button addCategoryButton;

        Dictionary<string, SettingsCategoryFragment> categories;

        public Dictionary<string, SettingsCategoryFragment> Categories { get { return categories ?? (categories = new Dictionary<string, SettingsCategoryFragment>()); } }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            currentView = inflater.Inflate(Resource.Layout.fragment_settings_categories_root, container, false);

            categoriesRoot = currentView.FindViewById<LinearLayout>(Resource.Id.settings_categories_root);
            addCategoryButton = currentView.FindViewById<Button>(Resource.Id.settings_add_category);

            addCategoryButton.Click += AddCategoryButton_Click;
            RatingManager.OnCategoriesUpdated += RatingManager_OnCategoriesUpdated;

            FillCategories();

            return currentView;
        }

        public override void OnDestroyView()
        {
            addCategoryButton.Click -= AddCategoryButton_Click;
            RatingManager.OnCategoriesUpdated -= RatingManager_OnCategoriesUpdated;

            base.OnDestroyView();
        }


        #region Working Process

        private void FillCategories()
        {
            FragmentTransaction tx = FragmentManager.BeginTransaction();
            foreach (String name in RatingManager.Instance.Categories)
            {
                AddCetegory(name, tx);
            }

            tx.AddToBackStack(null);
            tx.Commit();
        }

        private void AddCetegory(String name, FragmentTransaction tx = null)
        {
            bool isCommit = tx == null;
            tx = isCommit ? FragmentManager.BeginTransaction() : tx;

            SettingsCategoryFragment fragment = new SettingsCategoryFragment(name);
            tx.Add(Resource.Id.settings_categories_root, fragment);
            Categories.Add(name, fragment);

            if (isCommit)
            {
                tx.AddToBackStack(null);
                tx.Commit();
            }
        }

        private void DeleteCategory(String name)
        {
            FragmentTransaction tx = FragmentManager.BeginTransaction();
            tx.Remove(Categories[name]);
            tx.AddToBackStack(null);
            tx.Commit();
        }

        #endregion

        #region Events

        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            AddNewElementPopup popup = new AddNewElementPopup(GetString(Resource.String.add_category),
                GetString(Resource.String.add_category_hint), Context);

            popup.SubmitCallback = (newCategory) =>
            {
                RatingManager.Instance.AddCategory(newCategory);
                popup.Dismiss();
            };

            popup.SetContentView(Resource.Layout.popup_add_new_element);
            popup.Show();
        }

        private void RatingManager_OnCategoriesUpdated(string name, bool isAdded)
        {
            if (isAdded)
            {
                AddCetegory(name);
            }
            else
            {
                DeleteCategory(name);
            }
        }

        #endregion
    }
}