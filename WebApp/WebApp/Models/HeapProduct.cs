using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testPA3
{
    class HeapProduct
    {
        List<Product> productList = new List<Product>();
        private int _maxSize;
        //Heap oluşturulacak product listesi
        Product[] heapProductList;

        public HeapProduct(string productCat)
        {
            //Kullanıcı tarafından olay tetiklendiğinde gelen kategoriye göre ürünler alınacak
            _maxSize = productList.Count;
            heapProductList = new Product[_maxSize];
        }
        public void Insert(Product CategoryOfProduct)
        {
            if (_maxSize==heapProductList.Length)
            {
                throw new Exception("Product Heap is Full !");
            }
            else
            {
                heapProductList[_maxSize] = CategoryOfProduct;
                _maxSize++;
                ShiftUp(_maxSize-1);

            }
        }
        public int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        public void ShiftUp(int index)
        {
            int parentIndex;
            Product tempProduct;
            if (index!=0)
            {
                parentIndex = GetParentIndex(index);
                if (heapProductList[parentIndex].ProductPrice>heapProductList[index].ProductPrice)
                {
                    tempProduct = heapProductList[parentIndex];
                    heapProductList[parentIndex] = heapProductList[index];
                    heapProductList[index] = tempProduct;
                    ShiftUp(parentIndex);
                }
            }
        }
    }
}
