<%@ Page Title="" Language="C#" MasterPageFile="~/MsPage.Master" AutoEventWireup="true" CodeBehind="seasons.aspx.cs" Inherits="YZ.Web.Asp.seasons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/plugins.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#ms-sea").addClass("current-menu-item");
            $('div.body').css('min-height', (window.innerHeight - 129) + 'px');

            var chart = $('.chart'),
                chartNr = $('.chart-content'),
                chartParent = chart.parent();

            function centerChartsNr() {

                chartNr.css({
                    top: (chart.height() - chartNr.outerHeight()) / 2
                });

            }

            if (chart.length) {

                centerChartsNr();
                $(window).resize(centerChartsNr);

                chartParent.each(function () {

                    $(this).onScreen({
                        doIn: function () {
                            $(this).find('.chart').easyPieChart({
                                scaleColor: false,
                                lineWidth: 12,
                                size: 178,
                                trackColor: false,
                                lineCap: 'square',
                                animate: 2000,
                                onStep: function (from, to, percent) {
                                    $(this.el).find('.percent').text(Math.round(percent));
                                }
                            });
                        },
                    });

                    $(this).find('.chart').wrapAll('<div class="centertxt" />');

                });

            }
        });

    </script>
    <style>
        .chart {
            position: relative;
            display: inline-block;
            margin: 1em;
            width: 230px;
            height: 230px;
            text-align: center;
        }

        .chart-content {
            position: absolute;
            left: 0;
            top: 0;
            width: 100%;
        }

        .percent, .count-number, .count-number-done {
            color: #666;
            font-weight: 700;
            font-size: 2.333em;
            line-height: 1.34;
        }

        .chart-title, .count-subject {
            font-weight: 700;
            font-size: 0.889em;
        }

        .percent:after {
            content: '%';
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <div class="centertxt">
            <div class="chart" data-percent="85" data-bar-color="#FF6060" data-animate="2500">
                <div class="chart-content" style="top: 82.5px;">
                    <div class="percent">85</div>
                    <div class="chart-title">User Interface</div>
                </div>
                <!-- chart-content -->
                <%--   <canvas height="178" width="178"></canvas>--%>
            </div>
        </div>
    </div>

</asp:Content>
