﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Stashonizer.Application.ViewModels {

    public class ItemTextElement {
        public string Text { get; set; }
        public SolidColorBrush Forecolor { get; set; }
    }

    public class ItemDisplayProperty {

        List<ItemTextElement> Items { get; set; } 
        
        public ItemDisplayProperty() {
            Items = new List<ItemTextElement>();
            Items.Add(new ItemTextElement {Text = "Hallo", Forecolor = Brushes.Red});
            Items.Add(new ItemTextElement { Text = "du", Forecolor = Brushes.Yellow });
        }
    }

    public class ItemPopupViewModel : Screen {
        private UserControl _view;
        private string _headerLeftSource;
        private string _headerRightSource;
        private string _headerSource;
        private string _topText;
        private PoeItem _item;

        private ObservableCollection<ItemDisplayProperty> _properties = new ObservableCollection<ItemDisplayProperty>();

        public ObservableCollection<ItemDisplayProperty> ItemProperties {
            get { return _properties; }
        }

        public string HeaderLeftSource {
            get {
                return _headerLeftSource;
            }
            set {
                _headerLeftSource = value;
                NotifyOfPropertyChange(() => HeaderLeftSource);
            }
        }

        public PoeItem Item {
            get {
                return _item;
            }
        }

        public string HeaderRightSource {
            get { return _headerRightSource; }
            set {
                _headerRightSource = value;
                NotifyOfPropertyChange(() => HeaderRightSource);
            }
        }

        /// <summary>
        /// Sets the item for the popup
        /// </summary>
        /// <param name="item"></param>
        public void SetItem(PoeItem item) {
            _item = item;
            _properties.Add(new ItemDisplayProperty());
            Render();
        }

        public string HeaderSource {
            get { return _headerSource; }
            set {
                _headerSource = value;
                NotifyOfPropertyChange(() => HeaderSource);
            }
        }

        public string TopText {
            get {
                return _topText;
            }
            set {
                _topText = value;
                NotifyOfPropertyChange(() => TopText);
            }
        }


        [ImportingConstructor]
        public ItemPopupViewModel() {

        }

        internal double GetHeight() {
            return _view.ActualHeight * -1;
        }

        protected override void OnViewAttached(object view, object context) {
            _view = view as UserControl;
            base.OnViewAttached(view, context);
        }

        public void Render() {
            SetHeader(Item.rarity);
            
            //BuildItemProperties();
        }

        private void BuildItemProperties() {
            /*ItemProperties.Clear();
            foreach (var property in Item.properties) {
                if (property.name.Contains("%") && property.Values.Any()) {
                    var textEle = new ItemTextElement();
                    var parts = property.name.Split(new char[] {'%'});

                    foreach (var part in parts) {
                        if (Char.IsNumber(part[0])) {
                            var index = int.Parse(part[0].ToString());
                            ItemProperties.Add(GetTextElementForValue(property.Values[index]));
                            ItemProperties.Add(GetTextElementForText(part.Substring(1)));
                        }
                        else {
                            ItemProperties.Add(GetTextElementForText(part));
                        }
                    }
                }

                ItemProperties.Add(GetTextElementForText(Environment.NewLine));
            }*/
        }

        private ItemTextElement GetTextElementForValue(PropertyValue property) {
            var textEle = new ItemTextElement();
            textEle.Text = property.Value + "&#xD;";
            textEle.Forecolor = property.IsValueModifiedByAffix ? Brushes.DarkSlateBlue : Brushes.White;
            return textEle;
        }

        private ItemTextElement GetTextElementForText(string text) {
            var textEle = new ItemTextElement {Text = text, Forecolor = Brushes.Green};
            return textEle;
        }

        private void SetHeader(ItemRarity itemRarity) {
            string path = "/Stashonizer;component/A - Presentation/Images/header_{0}.png";
            HeaderLeftSource = string.Format(path, itemRarity.ToString().ToLower() + "_left");
            HeaderRightSource = string.Format(path, itemRarity.ToString().ToLower() + "_right");
            HeaderSource = string.Format(path, itemRarity.ToString().ToLower());
        }
    }
}
