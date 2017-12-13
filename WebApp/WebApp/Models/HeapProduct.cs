using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data;


namespace WebApp
{
    class HeapProduct
    {
        List<Product> productList = new List<Product>();
        ProductRepository productRepository = new ProductRepository();
        private int _arrSize;//kapasite tutar
        private int _heapSize;//eleman sayısı tutar
        //Heap oluşturulacak product listesi
        public Product[] heapProductList;
        private JsonAdapter jsonAdapter;
        public HeapProduct(string productCat)
        {
            //productListe ürünler alındı kategori filtremesi yapıldıktan sonra fiyata göre sort edildi ardından Heap arraye  eklendi
            productList = productRepository.GetAllProduct();
            var selectedProduct = productList.Where(x => x.Category == productCat);
            selectedProduct.OrderBy(x => x.Price);
            _arrSize = selectedProduct.Count();
            heapProductList = new Product[_arrSize];
            _heapSize = 0;
            foreach (var item in selectedProduct)
            {
                Insert(item);
            }
        }
        public void Insert(Product CategoryOfProduct)
        {
            if (_heapSize == heapProductList.Length)
            {
                throw new Exception("Product Heap is Full !");
            }
            else
            {
                heapProductList[_heapSize] = CategoryOfProduct;
                _heapSize++;
                ShiftUp(_heapSize - 1);

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
            if (index != 0)
            {
                parentIndex = GetParentIndex(index);
                if (heapProductList[parentIndex].Price > heapProductList[index].Price)
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
