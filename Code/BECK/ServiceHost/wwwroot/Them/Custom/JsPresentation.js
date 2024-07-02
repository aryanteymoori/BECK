let cookieName = "cart-items";
function addToCart(id, name, unitPrice, doubleUnitPrice, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }
    let count = $("#productCount").val();
    let currentProduct = products.find(x => x.id == id);
    if (currentProduct !== undefined) {
        currentProduct.count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice,
            doubleUnitPrice,
            picture,
            count
        }
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expiers: 2, path: "/" });
    updateCart();
}
function updateCart() {
    let products = $.cookie(cookieName);
    let product = JSON.parse(products);
    $("#cart_items_count").text(product.length);
    //$("#cart_items_count").text(JSON.parse($.cookie(cookieName)).length);
    var cartItemsWrapper = $("#Cart_Items_Wrapper");
    let productInCrat = "";
    for (let i = 0; i < product.length; i++) {
        productInCrat += `
                               <li class="mini-cart__product">
                        <a onclick="removeFromCart('${product[i].id}')" class="mini-cart__product-remove">
                            <i class="la la-remove"></i>
                        </a>
                        <div class="mini-cart__product-image">
                            <img src="Pictures/${product[i].picture}" alt="products">
                        </div>
                        <div class="mini-cart__product-content">
                            <a class="mini-cart__product-title" href="product-details.html">
                                ${product[i].name}
                            </a>
                            <span class="mini-cart__product-quantity"> <span>تعداد: ${product[i].count}</span> <span>قیمت: ${product[i].unitPrice}</span> </span>
                        </div>
                    </li> `
    }
    cartItemsWrapper.html(productInCrat);
}
function removeFromCart(id) {

    let products = $.cookie(cookieName);

    let product = JSON.parse(products);

    let itemToRemove = product.findIndex(x => x.id === id)

    product.splice(itemToRemove, 1);

    $.cookie(cookieName, JSON.stringify(product), { expiers: 2, path: "/" });

    updateCart();
}

function changeCartItemCount(id, totalId, count) {
    var products = $.cookie(cookieName);

    products = JSON.parse(products);

    var productIndex = products.findIndex(x => x.id == id);

    products[productIndex].count = count

    var product = products[productIndex];

    const newPrice = parseInt(product.doubleUnitPrice) * parseInt(count);

    $(`#${totalId}`).text(newPrice);

    $.cookie(cookieName, JSON.stringify(products), { expiers: 2, path: "/" });

    updateCart();
    //location.reload();
    var settings = {
        "url": "https://fileneed.ir/api/Inventory/",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "ProductId": id,
            "Count": count
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.isStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.prepend(`
                    <div id="${id}">
                         <div class="text-danger text-center mt-3 mb-3">محصول <strong>${response.productName}</strong> به تعداد خواسته شده در انبار موجود نیست</div>
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}