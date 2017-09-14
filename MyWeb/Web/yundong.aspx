<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="yundong.aspx.cs" Inherits="YZ.Web.Asp.yundong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="css/yd/bootstrap.css" rel='stylesheet' type='text/css' />
    <link href="css/yd/style.css" rel='stylesheet' type='text/css' />
    <link href='http://fonts.useso.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,300,600,700|Orbitron:400,500,700,900' rel='stylesheet' type='text/css'>
    <script src="js/yd/wow.min.js"></script>
    <link href="css/yd/animate.css" rel='stylesheet' type='text/css' />
     <script type="text/javascript">
         $(function () {
             $("#ms-exe").addClass("current-menu-item");

             $('div.body').css('min-height', (window.innerHeight - 129) + 'px');

             new WOW().init();
         });
    </script>
    <script type="application/x-javascript">
         <%--addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function ;(){ window.scrollTo(0,1); }--%>  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="body">
        <link rel="stylesheet" type="text/css" href="css/yd/component.css" />
       <%-- <script src="js/yd/modernizr.custom.js"></script>--%>

        <!--start-slide-bottom-->
        <div class="banner-bottom wow bounceIn animated" data-wow-delay="0.4s">
            <div class="container">
                <h2>ABOUT</h2>
                <p>
                    Dolor nunc vule putateulr ips dol consec.Donec sem ertet laciniate ultricie upie disse utes comete dolo lectus. fgilla itollicil tua ludin dolor nec met quam accumsan. Dolore condime netus lullam utlacus adipiscing ipsum molestie euismod lore estibulum vel libero ipsum sit
				amet sollicitudin ante. Aenean imperdiet aliquet hendreritunc interdum ullamcorper lec tuset pellentesqu enim interdum atuspendisse malesuada dignissim.
                </p>
            </div>
        </div>


        <!--classes-->
        <div class="clases-section">
            <div class="container">
                <h3>Our Classes</h3>
                <div class="class-grids">
                    <div class="col-md-6 class-grid wow zoomInLeft animated">
                        <img src="images/yd/c1.jpg" class="img-responsive" alt="" />
                        <div class="bottom-color">
                            <h4>Body Building </h4>
                            <p>with John Da Vinci</p>
                        </div>
                        <div class="class-bottom">
                            <div class="col-md-6 class-time ">
                                <ul class="time">
                                    <li><i class="sun"></i><span>Sunday October 14 </span></li>
                                    <li><i class="time"></i><span>12:30PM</span></li>
                                    <div class="clearfix"></div>
                                </ul>
                            </div>
                            <div class="col-md-6 class-text">
                                <div class="single-one"><span><a href="#"><i class="com"></i>20l</a></span></div>
                                <div class="single-one"><span><a href="#"><i class="four"></i>400</a></span></div>
                            </div>
                            <div class="clearfix"></div>
                            <a class="button" href="#">
                                <img src="images/yd/read.png" class="img-responsive" alt="" /></a>
                        </div>
                    </div>
                    <div class="col-md-6 class-grid wow zoomInRight animated">
                        <img src="images/yd/c2.jpg" class="img-responsive" alt="" />
                        <div class="bottom-color">
                            <h4>Cardio Practice</h4>
                            <p>with John Da Vinci</p>
                        </div>
                        <div class="class-bottom">
                            <div class="col-md-6 class-time">
                                <ul class="time">
                                    <li><i class="sun"></i><span>Sunday October 14 </span></li>
                                    <li><i class="time"></i><span>12:30PM</span></li>
                                    <div class="clearfix"></div>
                                </ul>
                            </div>
                            <div class="col-md-6 class-text">
                                <div class="single-one"><span><a href="#"><i class="com"></i>20l</a></span></div>
                                <div class="single-one"><span><a href="#"><i class="four"></i>400</a></span></div>
                            </div>
                            <div class="clearfix"></div>
                            <a class="button" href="#">
                                <img src="images/yd/read.png" class="img-responsive" alt="" /></a>
                        </div>
                    </div>
                    <div class="col-md-6 class-grid wow zoomInLeft animated">
                        <img src="images/yd/c3.jpg" class="img-responsive" alt="" />
                        <div class="bottom-color">
                            <h4>Athletic Training</h4>
                            <p>with John Da Vinci</p>
                        </div>
                        <div class="class-bottom">
                            <div class="col-md-6 class-time">
                                <ul class="time">
                                    <li><i class="sun"></i><span>Sunday October 14 </span></li>
                                    <li><i class="time"></i><span>12:30PM</span></li>
                                    <div class="clearfix"></div>
                                </ul>
                            </div>
                            <div class="col-md-6 class-text">
                                <div class="single-one"><span><a href="#"><i class="com"></i>20l</a></span></div>
                                <div class="single-one"><span><a href="#"><i class="four"></i>400</a></span></div>
                            </div>
                            <div class="clearfix"></div>
                            <a class="button" href="#">
                                <img src="images/yd/read.png" class="img-responsive" alt="" /></a>
                        </div>
                    </div>
                    <div class="col-md-6 class-grid wow zoomInRight animated">
                        <img src="images/yd/c1.jpg" class="img-responsive" alt="" />
                        <div class="bottom-color">
                            <h4>Body Building </h4>
                            <p>with John Da Vinci</p>
                        </div>
                        <div class="class-bottom">
                            <div class="col-md-6 class-time">
                                <ul class="time">
                                    <li><i class="sun"></i><span>Sunday October 14 </span></li>
                                    <li><i class="time"></i><span>12:30PM</span></li>
                                    <div class="clearfix"></div>
                                </ul>
                            </div>
                            <div class="col-md-6 class-text">
                                <div class="single-one"><span><a href="#"><i class="com"></i>20l</a></span></div>
                                <div class="single-one"><span><a href="#"><i class="four"></i>400</a></span></div>

                            </div>
                            <div class="clearfix"></div>
                            <a class="button" href="#">
                                <img src="images/yd/read.png" class="img-responsive" alt="" /></a>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <!--start-new-->
        <div class="new-section">
            <div class="container">
                <h3>Our news</h3>
                <div class="new">
                    <div class="col-md-6 new-text wow rollIn animated" data-wow-delay="0.4s">
                        <h5>THU 14 May, 2015</h5>
                        <a href="#">
                            <h4>How to lose fat fast</h4>
                        </a>
                        <p>Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                    </div>
                    <div class="col-md-6 welcome-img">
                        <a href="#" class="mask">
                            <img src="images/yd/n1.jpg" alt="image" class="img-responsive zoom-img"></a>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="new">
                    <div class="col-md-6 new-text two wow rollIn animated" data-wow-delay="0.4s">
                        <h5>THU 14 May, 2015</h5>
                        <a href="#">
                            <h4>Build your six pack</h4>
                        </a>
                        <p>Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                    </div>
                    <div class="col-md-6 new-img two">
                        <a href="#" class="mask">
                            <img src="images/yd/n3.jpg" alt="image" class="img-responsive zoom-img"></a>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="new">
                    <div class="col-md-6 new-text wow rollIn animated" data-wow-delay="0.4s">
                        <h5>THU 14 May, 2015</h5>
                        <a href="#">
                            <h4>How to lose fat fast</h4>
                        </a>
                        <p>Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                    </div>
                    <div class="col-md-6 welcome-img">
                        <a href="#" class="mask">
                            <img src="images/yd/n2.jpg" alt="image" class="img-responsive zoom-img"></a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
