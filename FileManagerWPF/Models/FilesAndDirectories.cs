using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileManagerWPF.Models
{
    class FilesAndDirectories
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string DateEdited { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; } = null;

        public ImageSource DisplayedImage
        {
            get
            {
                string image64 = "";
                if (Extension != null)
                {
                    switch (Extension.ToLower())
                    {
                        case "directory":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAABmJLR0QA/wD/AP+gvaeTAAAA60lEQVRIiWNgGA" +
                                "V0AozTa1wcWJiY1rOzsnzFpuDfv3+sf/7+W5rStKuImhazMDAwMEiL8jKoygpJ41J089HbpLn17pzJjTszqWox" +
                                "IaAuJ8z//fufmIXN3kkMjIz/KbLx37//P779i2P595+J9/uvPyzvPn3HqfbNhx8MXDxcPM7GGgzsbKwU2Xvm0p" +
                                "3/V28/lmZhZPin/OcfI/f3v7g9r6gowyAvLUqRheiAhYGBgUFCVJDBRE+FqgYTAkx0tW3U4lGLRy0etXjU4lGL" +
                                "Ry0eVBazMDAwMNx5+Jzh2ct3f+lh4bcfPwfMsyMUAAAjvEGrEXa+0gAAAABJRU5ErkJggg==";
                            break;
                        case ".bat":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAA" +
                                "AAlwSFlzAAAAzwAAAM8BRrlxRAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9y" +
                                "Z5vuPBoAAAKzSURBVFiF7ZdNSFVBGIaft6SgWphgZgZJiF2jRZBRbrRdtemHIKVN0KqW" +
                                "UUSQixYuCmoRERkF1SLQNv2Q2A9kC4VaVZQLI2iRLizDIDEi7Wsxc73j6Zzrvdefu+mF" +
                                "4c6835wz75kz3/eei5kRNqARGAZsFq0tet+kVsK/OAj0A80xsVxwAzgj6bOZXZtpcpyA" +
                                "EuCLmb0oZHVJY757RdKgmXVlm7+okEVyxGKgU9KWYgkAWA48krSuWAIAVgPdklYWSwBA" +
                                "HXBP0pKFEPAjgW8CTkfJuCyYLVqArTH8CWDNvAswsyFgKMpLaombv1BnIBHy5RdJa4HD" +
                                "uAq4FOie47V2A7+ATuC2mQ1OCZAk4ANQA/QCI8AoUAlsxqVSFF+B575fifMQ/AJJ2AZU" +
                                "Ax+BWgsMqIaMkZTGGFQ18JbphnMxiFcAk4BlMx7gVnB9jZmRDqSiAoAOoAdo8OO9EQFN" +
                                "gPyTAPR5/pxv+zy/PuDeBdenZhLwxo/P+vGBYM43XK3fCLT6+KmIwHbP7yDeslNmljUL" +
                                "LgH7gfOS6oC2INZlZpP+5rs89yDLvRKRWAfM7Ga6L6kMmAjCD/3vTmC7pDIzG5A0AGzI" +
                                "R0DiDkh6LemTpENm1oercOBS6bHvHwc2AT/9OO9dyPYKhDv9RwDMrB+Xej1mNiZpBbAK" +
                                "KCWTpvfzFZCtFL/EndrLAJJqgXIy29+M+/xKz20AXuG+JytyVpCUBZH8bQDe+3iV5+4G" +
                                "1/wBKjx/nTyyIL0D44GmZ5JGPFcOVOFyGeA3cMcVzmmOJ+CppFEyjrdHUgr3iuIwPrUD" +
                                "XmlvgtL5aL3pdUMzKsGl1UlgGXA1QXmhOOaf+gLwxMwmIHDDNCS1485BrH8XCkkdwHcz" +
                                "OxryRf8e+C+g6ALiKuEEUO8P41yinoyHZDBPf8/j2jDQGF3vL+T9wHVim3UDAAAAAElF" +
                                "TkSuQmCC";
                            break;
                        case ".pdf":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAA3NCSVQICAjb4U/gAAAA" +
                                "CXBIWXMAAAfpAAAH6QGUejxAAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jn" +
                                "m+48GgAAAXpQTFRF////29vb6+vi5eXT362o6Ojg6enf6urf3J+a2I6K4uLX3dzQ6eng" +
                                "3NrO0GJizEtMzExNzU5PzVFSzVJTzlRUzlRVzlVVzldXz1RVz1ZXz1lZz1pbz1tbz1xc" +
                                "0FhZ0Fla0F9f0GFg0GFh0Vxd0V1e0WRk0mBh0mlo0mxr03Fv03Fw1Hd11Wts1Xl41X16" +
                                "1X171m9w1nBx1oB+1oKA1oOB13Nz14aD14eE2HR12IyJ2Xh52Xp62Xp72ZGO2ZOP2dfK" +
                                "2n1+2n5/2pWS2piV239/23+A24CB25yX29rN3KKe3aWh342N37Gr37Ks37Sv4JGR4Lex" +
                                "4N/U4by24b234eDW4pma4sS+45uc456e48jB48nC5KCg5KCh5MvE5NDJ5dLL5dPL5tjQ" +
                                "59/X5+DX6K+v6OHY6OLZ6OPa6Obd6efe6ejf6eng67e37Ly87b6+7b/A78fI8MnJ8MvL" +
                                "8c3N8tHR89XW9NjY9Nra9t7e+enp+u3t/PT0/ff3/fn5cG1Z3QAAAA90Uk5TAA4aHThC" +
                                "R0htfNnr7PP7HqL7KAAAATBJREFUOI1jYGBgYOLJRgNczAzIgA1dPjuQF0UFO6YCexQV" +
                                "yAqysiAKUFQgKUhSNIEqQFaBpCBWXywLqgBJBZKCeEUlEBXiAQLcjFjcoKqLxGHFosBW" +
                                "D4nDjkWBumAwXgVx4j6yyfgUOFtmm5jhU6AUlp0k6+Xrk4pDQaRCgqemkIi0rQx2BVnm" +
                                "gqLGfmlJTlJ2WBVEaQgHZYBZmVjdECAs5Z6NAlAVZEmby6bjU5BtpJWYjVcBJqCeAg5t" +
                                "HIADqoCTHwfgRFEQHWHFrxYd7iLArxIdHa2DqaDGOt/fqtQ1pZDftNzKShlTQTW/W45V" +
                                "AT9/kZVpMVYramNKLEAK8rxNq3Jz5bFYYSjBD1JQaYDDhGogtqoILcvnx6HABojlHB2A" +
                                "zpc0QFPAwoc9GPhYgJIADt/lfB9xscQAAAAASUVORK5CYII=";
                            break;
                        case ".html":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAA3NCSVQICAjb4U/gAAAA" +
                                "CXBIWXMAAAfpAAAH6QGUejxAAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jn" +
                                "m+48GgAAAThQTFRF////29vb6+vi5eXT7bub6Ojg6enf6urf6quM6px54uLX3dzQ6eng" +
                                "3NrO7HlJ2dfK29rN4N/U4eDW6dfI6djJ6dnK6d/S6d/T6eHV6eLX6ePY6ebc6ejf6eng" +
                                "6quM6rKV6r6m6r+n6r+o6sCp6sWw6sq36tLC631P635Q639S64ph645m649n65Fp65h0" +
                                "65x465x5651765987GYw7Gcy7Gs27Gs37HNB7HVE7HVF7HZF7HZG7HhJ7Ws27W067W47" +
                                "7W887XJA7ndI73tM73xN73xO731P735Q74FV8INX8Ihd8Iph8Yxj8Y5m8Y9o8ZFq8pNt" +
                                "8pVw8pZx9KSE9KaH9KeI9KmL9KqN9a6R9rad9rie97yl972m98Gr+MSv+May+9zQ+97S" +
                                "++PZ/OXb/e/p/fHs/vbz/vn3S/FF2gAAAA90Uk5TAA4aHThCR0htfNnr7PP7HqL7KAAA" +
                                "ARlJREFUOI290NdWwkAQBuBVUcCCNbYYBbtiwx/sFVGMigiBGGvEEt//DcQYc5LsLN75" +
                                "38yeM9+2YYyx5k4lkPYW5k1bsK/0dvlEmAfdPkEBnyCBV9DAI3jQ1/OdjiYhcNL6FwgH" +
                                "gJyatev4JA3k5MKwvUhkEhSQk/NDzs54ZooAS3OD7uXxjWke7K2MuUDanOCBtLP6Kwa2" +
                                "16lHuqJ/K0V/U9qdsevymmAOyujPCSOiQXH5RxBZFCTigCgEibpAvQJ0VA3DuFRLwJGO" +
                                "m1Mf0O4BC7nya/5Ysw5wa8EsEwBqDdA+KukXHryZ5qcDnmqlu4YnPDy+5xqDk8p+HTzr" +
                                "VQ/IngEF4PACyOaBdAHnxeK1C0IxegyxUL35BRSvds4sRNBLAAAAAElFTkSuQmCC";
                            break;
                        case ".rar":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAA" +
                                "AAlwSFlzAAAAygAAAMoBawMUsgAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9y" +
                                "Z5vuPBoAAAJOSURBVFiF7ddPiM1RFAfwz0EK438KxaSJRjSKrIyFJKNEWUy2ClsUxUrK" +
                                "n40FC1bGSonSlJK/CyFKFtgYJTUjxcZGWHEtfvfxm5n33szw3mThW6d37/nfOffc+3tS" +
                                "SmoRpubfPqS/pN5qMSaogYiYjXu15H+A5dWYk2oEX4A7WNnABKpiWAIR0Ya7WNLs4Bjc" +
                                "gojowKPxCk6pAhExF0fwIhO8iogWLGxqAhExDfNwbIh8FnowvakJYD1uNitIPdQcw/FC" +
                                "1TEs4YPf5+Fv8W6sCVxHtwZORES05+W3lFI/VK7cLsOvzh04XIXfCPqONSmluhX4ivl5" +
                                "/RRPSrJl2GzwGXqBRZhT4l1RtBECG7Ei281j5DNQwS5MqRjhBPbieN5/xzZswoXMe5NS" +
                                "2hkRXSWdA7ivmLoCdVrQhTN53Y5bJdk1xTvx66XDzJzkp8zry77LPvfgYMV/3ddwBAyg" +
                                "pbQ/h0PowMUaNj8Uk1C2G3ULpJQqpZSv55687cMDXEIrjipKXbGLkl0rzpf9jroCEXEy" +
                                "IhZExDa8V4yo7HAVnmNGlt0q2V2IiIkRcRpvDR3rMZ6B14qHaX/mf85Bl2ad9izfYvAZ" +
                                "uIypuFH2P9IYVsMyxSneoOhla6aXJZ0BtKG/xNupqHY3bmNdRTDaBM6iM6+XKnr+GKux" +
                                "dojuYlxFZ0T0lvjdmI3Jg7TrtGCrYt6bcRMmrB+pBduxDw/r6PwpvqSUHlK/BbsVX7LP" +
                                "GhT0Y0rp1DBunRY0mvrG9L9gvPA/gf8J/DMJ9Cs+wZqJV9WYPwGrBjFu1eFHsQAAAABJ" +
                                "RU5ErkJggg==";
                            break;
                        case ".jpg":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAA3NCSVQICAjb4U/gAAAA" +
                                "CXBIWXMAAADdAAAA3QFwU6IHAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jn" +
                                "m+48GgAAAMlQTFRF////4ebmUL7nwcbL4ubnUL7oUb/oUr/oVsDpWMHpZMXrZsbraMfr" +
                                "acfrasjra8jsdsztecnoes7ue87ufc/ufs/ugdDug9HvhNHvhtLviNPvkMvjkdbxktbx" +
                                "k9fxldfxmdnymtryntvyp97zqN/0sLe9tuT1t+T2uOX2u8HGu+b2vef2vuf3v+f3w+n3" +
                                "yOv4yev4ytHYzOz4zez40dfc0e750+/51trd1tvf3vP74uXn4vT74/X77Pj88Pn99Pv+" +
                                "9fv++v3+/P7/8iVAcgAAAAV0Uk5TADy4wMmamUNaAAAA0ElEQVQ4y5XRZw+CMBCA4aIc" +
                                "dS+cuBfuvau4/v+PUjDBcC1V3k9N7kkubQlxUlTmKUxQaM76WDAMsOABEgLgFSLgERiM" +
                                "sMDggAXjOq7sJOAjZMARUmALOXiLH4AdfwHmDEMgLOsC8CkooAboQ7NCQSsOylQA4neY" +
                                "T9tnAzbrxiLvA+rQMtMWTWYiPmA/vuZqe5hdTA6UEoXTe0UzBqlHkXYmHOg9rRp0dfto" +
                                "7G7LDL8iSr/304K+g89TV11gt+LbeoD0N/8CqniuukARClUh5AUD4Xg1ug1BZAAAAABJ" +
                                "RU5ErkJggg==";
                            break;
                        case ".docx":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAA" +
                                "AAlwSFlzAAAA7AAAAOwBeShxvQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9y" +
                                "Z5vuPBoAAANESURBVFiF7ZdLaFRXGIC//87DmMZMZjRGEyXB1EnMLW3BEswYa2ckUC0F" +
                                "F1ooRbClhtYuXAgFkbootAuR0p2KXXZRbOmDQh8LnQkqob6woUnatKJJY1M0yUySmZTM" +
                                "4xwX85BpopmbuYUu/OBy4Nz/cf7//OcllEnT/nBFws1zylC3Jk7tuGNV32lVoe6tS6u1" +
                                "SgZQdGokMCtsRuMW5fgU2Gf7AOreuLBBG6oTQ29F06kyqU0ggmT/65ycaJZZdQ7kzeTo" +
                                "vupapaafFnF0gt4KBIFVJVnSjCL0WnGu4ZZz5Zs9IQdql0YCouObtRjuB3H9h2jWibDX" +
                                "aYj+QSOu3IiWjtB778wLr5QqXtsd2YumwwBc5fgtF8ur4KFoOmoPRM4uJiboZDptHMnn" +
                                "27BtAEvEvgxYr4Et8H/KQINvGfufX8MHXw8XCRzsauCrK/cYiyULfTuf9aEU/Ng3+UCw" +
                                "3BpYXe3iYFcD/rWVBWH/2kqO7m7k1UBdkZF3X26kvXlFyVE+ikIG+kYSRBMpQm01DI3N" +
                                "AhAyvYX2o+/+BKDe68a/ZjnvnZ36d2jl1UBGaSKDUwTNmoJQsM3D3ekkzzRWsXJFdrvY" +
                                "8ZSPxFyGyzenLca6MEWrINwf5cRrT1JV4UBpaG+u5tgXt3l/TxPbWz18eWWckOnl0tA0" +
                                "ybQqtmTHPhAeiOF0CAG/h20tHpwOg+9vTHD55gxB04fbKQQ2VnO+P1p25HmKMjAZT9E3" +
                                "EidkehHg5+E44zMpwgMx3umqJ+CvoarCQc/gAgOwax8490uUkOklaHoJD2Qdne+P4qty" +
                                "cejFBn7/+x9GxueshfkI5u2EkcEYh19an3McA2BobJY7k3O0N1dz+txfC1uy6yy4cXuG" +
                                "iZlUYTryhAdiRa1dzMuA0vBZ710guzTzfHt9gu2bavjpj4csvyXWwIKH0YffDM/ru/hb" +
                                "jC3HrpVqv2Qe3wce3wek9kDEnju41XeBZh1Ch4HIx8B1IL6Yjq0IoxqOF72MPG9f8Loz" +
                                "aVMro01EbQAxEdrQNLH4dH1upQbyFBXh1MltUeBi7itQ3321MpVJtCD4NbSK6FbAD7QA" +
                                "T2QDIm3VeU6vPHyv96x3OVVLSvPr5CfBUav69wGMQ0bXf47aywAAAABJRU5ErkJggg==";
                            break;
                        case ".exe":
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAA" +
                                "AAlwSFlzAAAA7AAAAOwBeShxvQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9y" +
                                "Z5vuPBoAAALjSURBVFiFxZZLTxNRFMf/d2gGOhRomzZFNDwaMLEaeSgrjMSFNeED6MLH" +
                                "wrA3xETiJzAsjI+10Q3GQIwfAA1Roys1FlASI0IJtkVoS7VQbOnMcXElQzudMJeW8E9u" +
                                "etpz7zm/e86dOwUOWMzMEZrZChKkk4xQJRp0NaFmz5+T7+8Z4PN0fhTAZdHE2/q+QPiT" +
                                "pveDV+Qzu82Vin8IzWwFy0m+rZU4+h6N5t4JAxCkU+Umr2I6xOOnuddCANC06nIB6uv1" +
                                "zi6vov/JM/NKGAEqII8baGrUIWK/0GcGIQxApNvzi4T5RSrp6www9HZJOOpn8Lcw2GtY" +
                                "38Skeqs4nk0kuaoBn6Y1+DwMLUcYvv3gGf0tDEtRQmSZcLpTgu3/g+txAx73jgeNNKUs" +
                                "gHiCkEgCiSRhJa5v90NIQzzJ7USS4POaXi8GCbXA52UIdPDg2wl32oEOJpQcEKjA2m9g" +
                                "LUXIbPLv9hqG3i7e949TwOZfQnqDnwuXk8HVUGGAeJIwt6CX3ecFahW+W5+HEP4JLEW5" +
                                "v70NcDVYq4QlgGwOaHAwHGsH8nkgvcE/E0kCY4CqAo1ehrpawGYDFDtDNgdUyxUCyOWA" +
                                "vArIMoMsA4qigwGA21U4P6/yNRUDKNbhQwydJwrP79QXDZEYmayoMEAkRojE1L0sNWhf" +
                                "rmIRWaqALAN1DrHAsoX+Wwaolq0dqL3owFtw4ACGFkyFWPN6Ruw+tyqHguZdAebmJP/X" +
                                "2f0BOB4g/64AZhoYAAavc/vBQ37lNjXxl1E0CtTYC/1v3lqLaxmgzgGMjfMBAE4ncO0q" +
                                "t28MAReChX6rEjqEly4C/We5nUoBs7N8pFJGv1UZKkASsmaTx8b10jqdQCCg28X+UioV" +
                                "21ABh8ImSy1Or/MdvnjOd9nTDbx8xUdPt9FfSqVilzzuQze1hXCYtZrvRVytrRS+d1dq" +
                                "swQAAHdGaHg9Q0E1zwz/ZEVUZaOMQ2ETt4fZSDlx9k3/ADqa+bHSBpUwAAAAAElFTkSu" +
                                "QmCC";
                            break;
                        default:
                            image64 =
                                "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAA" +
                                "AAlwSFlzAAAA7AAAAOwBeShxvQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9y" +
                                "Z5vuPBoAAAFMSURBVFiF7dc9L0RBFMbxn5fQiYRKwiooRaKW2Gg1voZCBFFQ+S5KOo23" +
                                "WqImKr0gyC7NSmQVdyXLveLeuWND4klOM5n7nP+cOZOZyy9UBSdooBkYNVRDAU6wib5Q" +
                                "gxbEbShEo2Tyd4BqKESzZPJ2j1ncYL7TAO1VrCpYiRgAx9gKhYgBUMGR9EmqdQogt3f3" +
                                "DybLpVCAUeyhjmccYCYW1HdbMIprrGAQA1jFAyZLeueatNtK/lnrkqr8OEAdQxnjQ7gr" +
                                "6h3SA0u4zxjvxmuAX0qhx7BjW/BZw1jDI6Y6DbCMJ+xjOpZ3EYDtVuRVyru3wMdZymrG" +
                                "0vozd8E4LnGFiVCTMgCLOMMpFkr4pJR3Cyq4wLmkGtG8/0wPRNE/QBbAi/I/JlnqlzxS" +
                                "P6gnY+IcRiRHLMr1KlnQBrqw893kr57UZaKBQ4xFWlA8vQFrCXP60Rx8+wAAAABJRU5E" +
                                "rkJggg==";
                            break;

                    }

                    byte[] binaryData = Convert.FromBase64String(image64);
                    Image source = new Image();
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(binaryData);
                    bi.EndInit();
                    source.Source = bi;
                    ImageSource image = new WriteableBitmap(bi);
                    return image;

                }
                return null;
            }
        }
    }
}
