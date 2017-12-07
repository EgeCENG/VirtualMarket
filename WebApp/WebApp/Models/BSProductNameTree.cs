using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    class BSProductNameTree
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
        //Yeni gelen düğüm ile ilgili alan ağaçta varmı o kontrol ediliyor.
        public bool Search(BSTProductNode localRoot,string pName)
        {
            if (localRoot==null)            
                return false;
            if (localRoot.subCategory==pName)            
                return true;
            else if (pName.CompareTo(localRoot.subCategory)==-1)
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
        private void AddProduct(BSTProductNode localRoot,Product newProduct)
        {
            if (newProduct.Name==localRoot.subCategory)
            {
                localRoot.productInfo.Add(newProduct);
                return;
            }
            if (localRoot!=null)
            {
                AddProduct(localRoot.leftChild, newProduct);
                AddProduct(localRoot.rightChild, newProduct);
            }          
        }
        //Yeni düğüm ekleme işlemi
        public void InsertNode(BSTProductNode newNode)
        {
            if (_root==null)
            {
                _root = newNode;
            }
            else
            {
                //Düğümün ilgili alanı (Örn: Dizüstü Bilgisayar) ağaçta var ise o düğüme yeni ürünün Marka,Model vs bilgileri ürün listesine ekleniyor
                if (Search(_root,newNode.subCategory))
                {
                    AddProduct(_root, newNode.productInfo[0]);
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
                            if (current==null)
                            {
                                parent.leftChild = newNode;
                                return;
                            }
                        }
                        else
                        {
                            current = current.rightChild;
                            if (current==null)
                            {
                                parent.rightChild = newNode;
                                return;
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
