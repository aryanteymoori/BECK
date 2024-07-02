function confirmComment(id, path) {
    var settings = {
        "url": `https://${path}/api/ChangeCommentState/Confirm/${id}`,
        "method": "POST",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {
        var comment = response.find(x => x.id == id);
        if (comment.isConfirmed) {
            var state = $(`#state_${id}`)
            state.html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
};

function cancelComment(id,path) {
    var settings = {
        "url": `https://${path}/api/ChangeCommentState/Cancel/${id}`,
        "method": "POST",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {
        var comment = response.find(x => x.id == id);
        if (comment.isCanceled) {
            var state = $(`#state_${id}`)
            state.html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
};


function orderPay(id,path) {
    var settings = {
        "url": `https://${path}/api/ChangeOrderState/Paid/${id}`,
        "method": "POST",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {
        var order = response.find(x => x.id == id);
        if (order.isPaid) {
            $(`#orderstate_${id}`).html(` <strong class="text-success">پرداخت شده</strong>`)
        }
    });
}



function orderCancel(id,path) {
    var settings = {
        "url": `https://${path}/api/ChangeOrderState/Canceled/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {
        var order = response.find(x => x.id == id);
        if (order.isCanceled) {
            $(`#orderstate_${id}`).html(` <strong class="text-danger">کنسل شده</strong>`)
        }
    });
}




function restorePicture(id,path) {
    var settings = {
        "url": `https://${path}/api/ProductPicture/RestoreProductPicture/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (!respose.isDeleted) {
            $(`#productPictureState_${id}`).html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
}



function deletePicture(id,path) {
    var settings = {
        "url": `https://${path}/api/ProductPicture/DeleteProductPicture/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (respose.isDeleted) {
            $(`#productPictureState_${id}`).html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
}






function restoreSlide(id,path) {
    var settings = {
        "url": `https://${path}/api/Slider/RestoreSlider/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (!respose.isDeleted) {
            $(`#SlideState_${id}`).html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
}




function deleteSlide(id,path) {
    var settings = {
        "url": `https://${path}/api/Slider/DeleteSlider/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (respose.isDeleted) {
            $(`#SlideState_${id}`).html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
}












function deleteDiscount(id,path) {
    var settings = {
        "url": `https://${path}/api/ColleagueDiscount/DeleteColleagueDiscount/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (respose.isDeleted) {
            $(`#commentState_${id}`).html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
}



function restoreDiscount(id,path) {
    var settings = {
        "url": `https://${path}/api/ColleagueDiscount/RestoreColleagueDiscount/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (!respose.isDeleted) {
            $(`#commentState_${id}`).html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
}











function restoreArticleCategory(id,path) {
    var settings = {
        "url": `https://${path}/api/ArticleCategory/RestoreArticleCategory/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (!respose.isDeleted) {
            $(`#articleCategoryState_${id}`).html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
}









function deletArticleCategory(id,path) {
    var settings = {
        "url": `https://${path}/api/ArticleCategory/DeleteArticleCategory/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (respose.isDeleted) {
            $(`#articleCategoryState_${id}`).html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
}






















function restoreArticel(id,path) {
    var settings = {
        "url": `https://${path}/api/Article/RestoreArticle/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (!respose.isDeleted) {
            $(`#articleState_${id}`).html(`<i class="fa fa-check fa-2x text-success"></i>`)
        }
    });
}









function deleteArticle(id,path) {
    var settings = {
        "url": `https://${path}/api/Article/DeleteArticle/${id}`,
        "method": "Post",
        "timeout": 0,
    };

    $.ajax(settings).done(function (respose) {
        if (respose.isDeleted) {
            $(`#articleState_${id}`).html(`<i class="fa fa-times fa-2x text-danger"></i>`)
        }
    });
}