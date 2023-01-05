
# About
このEditor拡張は、自動で動画、音楽、テクスチャの設定を行います。



# Build Environment

| Title |   Version   | Memo  |       |
| :---: |:-----------:| :---: | :---: |
|  OS   |             |       |       |
| Unity | 2021.3.16f1 |       |       |
|       |             |       |       |


# Target Platform

| Platform | Support | Memo  |       |
| :------: |:-------:| :---: | :---: |
| Windows  |    ○    |       |       |
|  WebGL   |     ○    |       |       |
|   Mac    |     ○    |       |       |
|   iOS    |     ○    |       |       |
| Android  |     ○    |       |       |
|   tvOS   |     ○    |       |       |
|  WebGL   |     ○    |       |       |


# Detail

* 初回インポート時のみ、最低限のフォーマット設定のみを行うようになっています。
* CommonConfig.csの定義によって、どのディレクトリをどのフォーマット設定にするか決めています。
* 上記ディレクトリに該当しないファイルには、最低限の共通設定を行います。


# Setting

* CommonConfig.csのディレクトリ設定を変更することで、どれをUIとして処理するかなどを変更できます。
* settingwindowのSkipAllは、このEditor拡張のフォーマットをスキップするか。
* settingwindowのEveryImportTimeは、初回インポート以外でもフォーマット設定するか。


# Licence

MIT
