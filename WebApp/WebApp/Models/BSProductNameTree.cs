using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class BSProductNameTree
    {
        private BSTProductNode _root;
        private int _subCategoryCount = 0;
        //Constructor
        public BSProductNameTree()
        {
            _root = null;
            _subCategoryCount = 0;
        }
        public BSTProductNode GetRoot()
        {
            return _root;
        }
        public void InOrder(BSTProductNode localRoot)
        {
            if (localRoot != null)
            {
                InOrder(localRoot.leftChild);
                localRoot.DisplayNode();
                InOrder(localRoot.rightChild);
            }
        }
        public void PreOrder(BSTProductNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.DisplayNode();
                InOrder(localRoot.leftChild);
                InOrder(localRoot.rightChild);
            }
        }
        public void PostOrder(BSTProductNode localRoot)
        {
            if (localRoot != null)
            {
                InOrder(localRoot.leftChild);
                InOrder(localRoot.rightChild);
                localRoot.DisplayNode();
            }
        }
        int tempDepth;
        public int FindDepth(BSTProductNode localRoot, int d)
        {
            if (localRoot != null)
            {
                d++;
                FindDepth(localRoot.leftChild, d);
                FindDepth(localRoot.rightChild, d);
                if (d > tempDepth)
                {
                    tempDepth = d;
                }
            }
            return tempDepth;
        }
        int count = 0;
        public int CountOfProduct(BSTProductNode localRoot)
        {
            if (localRoot != null)
            {
                count += localRoot.productList.Count;
                CountOfProduct(localRoot.leftChild);
                CountOfProduct(localRoot.rightChild);
            }
            return count;
        }
        List<int> levelOfProductCount = new List<int>();
        public void FindLevelOfProductCount(BSTProductNode localRoot, int d)
        {
            if (localRoot != null)
            {
                d++;
                levelOfProductCount.Add(localRoot.productList.Count);
                FindLevelOfProductCount(localRoot.leftChild, d);
                FindLevelOfProductCount(localRoot.rightChild, d);
            }
        }
        //Müşteri modelülü için marka model isim parametreleri için arama 
        //sonuçları liste ekler
        public List<Product> searchResults = new List<Product>();
        public void ProductSearchName(BSTProductNode localRoot, string name)
        {
            if (localRoot != null)
            {
                if (name.Equals(localRoot.subCategory))
                {
                    foreach (var item in localRoot.productList)
                    {
                        searchResults.Add(item);
                    }
                    ProductSearchName(localRoot.leftChild, name);
                    ProductSearchName(localRoot.rightChild, name);
                }
            }
        }
        public void ProductSearchBrand(BSTProductNode localRoot, string brand)
        {
            if (localRoot != null)
            {
                foreach (var item in localRoot.productList)
                {
                    if (brand.Equals(item.Brand))
                    {
                        searchResults.Add(item);
                    }
                }
                ProductSearchBrand(localRoot.leftChild, brand);
                ProductSearchBrand(localRoot.rightChild, brand);
            }
        }
        public void ProductSearchModel(BSTProductNode localRoot, string model)
        {
            if (localRoot != null)
            {
                foreach (var item in localRoot.productList)
                {
                    if (model.Equals(item.Model))
                    {
                        searchResults.Add(item);
                    }
                }
                ProductSearchBrand(localRoot.leftChild, model);
                ProductSearchBrand(localRoot.rightChild, model);
            }
        }
        public void DeleteName(string name)
        {
            _root = DeleteNode(_root, name);
        }
        public BSTProductNode DeleteNode(BSTProductNode localRoot, string name)
        {
            if (localRoot == null)
            {
                return localRoot;
            }
            if (name.CompareTo(localRoot.subCategory) == -1)
            {
                localRoot.leftChild = DeleteNode(localRoot.leftChild, name);
            }
            else if (name.CompareTo(localRoot.subCategory) == 1)
            {
                localRoot.rightChild = DeleteNode(localRoot.rightChild, name);
            }
            else
            {
                if (localRoot.leftChild == null)
                {
                    return localRoot.rightChild;
                }
                else if (localRoot.rightChild == null)
                {
                    return localRoot.leftChild;
                }
                localRoot.subCategory = minValue(localRoot.rightChild);
                localRoot.rightChild = DeleteNode(localRoot.rightChild, localRoot.subCategory);
            }
            return localRoot;
        }
        //çocuk düşüme sahip olan düğüm silindiğinde yeri getirilecek node bulur
        string minValue(BSTProductNode localRoot)
        {
            string minv = localRoot.subCategory;
            while (localRoot.leftChild != null)
            {
                minv = localRoot.leftChild.subCategory;
                localRoot = localRoot.leftChild;
            }
            return minv;
        }
        public void DeleteBrand(BSTProductNode localRoot, string brand)
        {
            if (localRoot != null)
            {
                foreach (var item in localRoot.productList)
                {
                    if (brand.Equals(item.Brand))
                    {
                        localRoot.productList.Remove(item);
                        if (localRoot.productList.Count == 0)
                        {
                            break;
                        }
                    }
                }
                DeleteBrand(localRoot.leftChild, brand);
                DeleteBrand(localRoot.rightChild, brand);
            }
        }
        //Modele göre silme
        public void DeleteModel(BSTProductNode localRoot, string model)
        {
            if (localRoot != null)
            {
                foreach (var item in localRoot.productList)
                {
                    if (model.Equals(item.Model))
                    {
                        localRoot.productList.Remove(item);
                    }
                }
                DeleteModel(localRoot.leftChild, model);
                DeleteModel(localRoot.rightChild, model);
            }
        }

        //Yeni gelen düğüm ile ilgili alan ağaçta varmı o kontrol ediliyor.
        public bool Search(BSTProductNode localRoot, string pName)
        {
            if (localRoot == null)
                return false;
            if (localRoot.subCategory == pName)
                return true;
            else if (pName.CompareTo(localRoot.subCategory) == -1)
            {
                return Search(localRoot.leftChild, pName);
            }
            else if (pName.CompareTo(localRoot.subCategory) == 1)
            {
                return Search(localRoot.rightChild, pName);
            }
            return false;
        }
        //Eğer Ürün Adına Ait Node Ağaçta varsa o zaman yeni Node un ürün infosu ağactaki ilgili düğümün ürün listine ekleniyor.
        private void AddProduct(BSTProductNode localRoot, Product newProduct)
        {
            if (localRoot != null)
            {
                if (newProduct.Name == localRoot.subCategory)
                {
                    localRoot.productList.Add(newProduct);
                    return;
                }
                AddProduct(localRoot.leftChild, newProduct);
                AddProduct(localRoot.rightChild, newProduct);
            }
        }
        //Yeni düğüm ekleme işlemi
        public void InsertNode(BSTProductNode newNode)
        {
            if (_root == null)
            {
                _root = newNode;
            }
            else
            {
                //Düğümün ilgili alanı (Örn: Dizüstü Bilgisayar) ağaçta var ise o düğüme yeni ürünün Marka,Model vs bilgileri ürün listesine ekleniyor
                if (Search(_root, newNode.subCategory))
                {
                    AddProduct(_root, newNode.productList[0]);
                }
                //İlgili alan yok ise standart BST ekleme işlemi yapılıyor
                else
                {
                    BSTProductNode current = _root;
                    BSTProductNode parent;
                    while (true)
                    {
                        parent = current;
                        if (newNode.subCategory.CompareTo(_root.subCategory) == -1)
                        {
                            current = current.leftChild;
                            if (current == null)
                            {
                                parent.leftChild = newNode;
                                return;
                            }
                        }
                        else
                        {
                            current = current.rightChild;
                            if (current == null)
                            {
                                parent.rightChild = newNode;
                                return;
                            }
                        }
                    }

                }
            }
        }
        //Ağaç dengeleme Müşteri Modülü için
        List<BSTProductNode> productListForBalance = new List<BSTProductNode>();
        //Ağaçtaki tüm veriler sort edilerek listeye alındı
        public void AddToArray(BSTProductNode localRoot)
        {
            if (localRoot != null)
            {
                AddToArray(localRoot.leftChild);
                productListForBalance.Add(localRoot);
                AddToArray(localRoot.rightChild);
            }
        }
        //listenin orta elemanından başlayarak ağaç dengelendi
        public void BalanceTree(int low, int high)
        {
            if (low == high)
                return;
            int mid = (low + high) / 2;
            InsertNode(productListForBalance[mid]);
            BalanceTree(mid + 1, high);
            BalanceTree(low, mid);
        }
        //Product update
        public void UpdateProduct(BSTProductNode localRoot, Product updatedProduct)
        {
            if (localRoot != null)
            {
                foreach (Product item in localRoot.productList)
                {
                    if (updatedProduct.Id.Equals(item.Id))
                    {
                        item.Category = updatedProduct.Category;
                        item.Brand = updatedProduct.Brand;
                        item.Model = updatedProduct.Model;
                        item.Count = updatedProduct.Count;
                        item.Price = updatedProduct.Price;
                    }
                }
                UpdateProduct(localRoot.leftChild, updatedProduct);
                UpdateProduct(localRoot.rightChild, updatedProduct);
            }
        }
        //Verilen fiyat aralıklarında arama 
        public List<Product> SearchByPrice(BSTProductNode localRoot, double minPrice, double maxPrice)
        {
            List<Product> selectedProducts = new List<Product>();
            if (localRoot != null)
            {
                foreach (var item in localRoot.productList)
                {
                    if (item.Price >= minPrice && item.Price <= maxPrice)
                    {
                        selectedProducts.Add(item);
                    }
                }
            }
            return selectedProducts;
        }
    }
}
