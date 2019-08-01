# 多国语化便利工具
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2015-red.svg)](https://www.visualstudio.com/zh-hans/) ![Language](https://img.shields.io/badge/Language-C%23%20-orange.svg) ![CSCOM](https://img.shields.io/badge/CSCOM(APS)-3.0.0.0-orange.svg)
## Summary
一个用于将Winform按照辞书进行迅速翻译的工具。使用该工具，告别传统做多国语化的农耕时代，工具化进入工业时代。

## 使用方法
1.准备辞书，辞书的格式请务必遵循以下的格式。
<li>辞书为excel2017的文件格式.xlsm或.xlsx。</li>
<li>拥有两个sheet页，这么做的原因是希望能够将修改的履历记录下来，所以第一个sheet页为履历页。第二个sheet页为真正的辞书放置页，名字可自由命名。</li>
<li>第二个sheet页的标准格式。第一行一定为title名，且必须包含ci，class_name，resource_key这几页，后面的对应的语言翻译标题名为语言缩略字+“_value”。 请设定好配置文件中定义的所有语言。可参考下图：</li>

![](https://github.com/wemanclh/WinFormEasyTranslate/tree/master/img/dictionary_title_format.png)
![](https://github.com/dathlin/ClientServerProject/raw/master/软件系统客户端模版/screenshots/client13.png)
