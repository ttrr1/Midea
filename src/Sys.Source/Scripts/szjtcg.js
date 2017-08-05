$(function () {

    //dynamic
    $('.Activity_box li').click(function () {
        $('#zhezhao').show()
        $('#add1').show()

        return false;
    });


    $('.close').click(function () {
        $('#zhezhao').hide()
        $('#add1').hide()
        return false;
    });





    $('.notice li').hover(function () {
        $(this).addClass('active');
    }, function () {
        $(this).removeClass('active');
    })

    $('.m_04 dl').hover(function () {
        $(this).find('.outbox').show();
    }, function () {
        $(this).find('.outbox').hide();
    })



    $('.chaxun tr').hover(function () {
        $(this).addClass('bgblue');
    }, function () {
        $(this).removeClass('bgblue');
    })



    $(".tbStu tr:nth-child(odd)").addClass("tab_bg");

    //dynamic
    $('.box_36_title a').click(function () {
        $('.box_36_title a').removeClass('active')
        $(this).addClass('active')
        $('.none').hide();
        $('#' + $(this).attr('data-to')).show()
    });

    ;
    $('.kaoqin_more01').click(function () {
        //		$('#zhezhao').show()
        //		$('#add1').show()
        //		return false;
    });
    $('.close_btn').click(function () {
        $('#zhezhao').hide()
        $('#add1').hide()
        return false;
    });

    $('.close_btn_8').click(function () {
        $('#zhezhao').hide()
        $('#add1').hide()
        return false;
    });

    $('.m_l').hover(function () {
        $('.zz_l').hide();
    }, function () {
        $('.zz_l').show();
    });

    $('.m_r').hover(function () {
        $('.zz_r').hide();
    }, function () {
        $('.zz_r').show();
    });




    

});


