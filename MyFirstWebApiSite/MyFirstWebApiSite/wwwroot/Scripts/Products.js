
const getAllProducts = async () => {
    const responsePost = await fetch('api/product');
    if (responsePost.ok) {
        const data = await responsePost.json();
        drawProducts(data)

        console.log(data)
        const minPrice = Math.min(...data.map(item => item.price));
        const maxPrice = Math.max(...data.map(item => item.price));

        const minPriceInput = document.getElementById('minPrice');
        const maxPriceInput = document.getElementById('maxPrice');

        minPriceInput.value = minPrice;
        maxPriceInput.value = maxPrice;

    
    }
    else {
        alert("There are no products...");
     
    }

}

    const drawProducts = (products) => {


        const template = document.getElementById('temp-card');
        products.forEach(product => {
            const card = template.content.cloneNode(true);

            card.querySelector('h1').textContent = product.name;
            card.querySelector('.price').textContent = product.price;
            card.querySelector('.description').textContent = product.description;
            card.querySelector('.name').textContent = product.productName;
            card.querySelector('img').src = '../Images/' + product.imageUrl;
            card.querySelector('button').addEventListener('click', () => addToBasket(product));

            document.getElementById("PoductList").appendChild(card);
        });

        const productsArray = JSON.parse(sessionStorage.getItem('Basket')) || [];
        let sum = 0;

        productsArray.forEach(p => { sum += p.quantity; })
        document.getElementById('ItemsCountText').textContent = sum;

        document.getElementById('counter').textContent = products.length;

    }

    const getAllCategories = async () => {

        const responsePost = await fetch('api/categoey');
        if (responsePost.ok) {
            const data = await responsePost.json();
            //drawProducts(data)
            drawCategories(data)


        }
        else {
            alert("aaa");
        }

    }
    getAllCategories()

    const drawCategories = (categories) => {

        const template = document.getElementById('temp-category');

        categories.forEach(category => {
            const card = template.content.cloneNode(true);

            card.querySelector('.opt').id = category.categoryId;
            card.querySelector('.opt').value = category.categoryName;
            card.querySelector('.OptionName').textContent = category.categoryName;
            card.querySelector('.opt').addEventListener('change', (e) => { categoriesFilter(e, category) });

            document.getElementById("categoryList").appendChild(card);
        });
    }
    var categoriesArr = []


    const categoriesFilter = (e, category) => {

        if (e.target.checked) {
            categoriesArr.push(category.categoryId)
        }
        else {
            const x = categoriesArr.indexOf(category.categoryId);
            categoriesArr.splice(x, 1);
        }

        filterProducts()
    }

    const addToBasket = (product) => {
        const productsArray = JSON.parse(sessionStorage.getItem('Basket')) || [];

        const index = productsArray.findIndex(p => p.productId == product.productId)

        if (index != -1) {
            productsArray[index].quantity += 1;
        }
        else {
            product.quantity = 1;
            productsArray.push(product);
        }

        sessionStorage.setItem('Basket', JSON.stringify(productsArray));

        let sum = 0;

        productsArray.forEach(p => { sum += p.quantity; })
        document.getElementById('ItemsCountText').textContent = sum;
    }

    const clearBasket = () => {

        sessionStorage.removeItem('Basket');
    }

const filterProducts = async () => {
    const name = document.getElementById('nameSearch').value;
    const min = document.getElementById('minPrice').value;
    const max = document.getElementById('maxPrice').value;
    var categories = '';
    categoriesArr.forEach(x => categories += `&category=${x}`);

    const responsePost = await fetch(`api/product?min=${min}&max=${max}&description=${name}${categories}`);

    const productList = document.getElementById('PoductList');
    productList.replaceChildren();  // Clear existing items

    if (responsePost.ok) {
        const data = await responsePost.json();

        if (data.length != 0) {
            drawProducts(data);
        } else {
            const noItemsMessage = document.createElement('p');
            noItemsMessage.textContent = "...אין פריטים תואמים לחיפוש";
            noItemsMessage.style.textAlign = 'center';
            productList.appendChild(noItemsMessage);
            document.getElementById('counter').textContent = 0;
        }
    } else {
        const errorMessage = document.createElement('p');
        errorMessage.textContent = "Something is wrong with the filter... we are so sorry.";
        errorMessage.style.textAlign = 'center';
        productList.appendChild(errorMessage);
    }
};


getAllProducts();