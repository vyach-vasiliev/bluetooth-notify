# Правовой пакет Bluetooth Notify

Актуализация: 11 сентября 2026 г. Версия пользовательских документов: 1.2. Этот документ — инженерная карта принятых решений, а не юридическое заключение.

## Что реализовано

- Единая пользовательская страница содержит отдельные Политику конфиденциальности, Условия использования и Отказ от ответственности на `en`, `ru`, `de`, `fr`, `es`, `pt`, `ja`, `ko`, `zh-CN`.
- Страница автономна: без внешних библиотек, cookie, аналитики и сетевых запросов. Та же папка включается в сайт и publish приложения, чтобы тексты не расходились.
- В настройках приложения находятся три локализованные ссылки. Документ открывается с языком текущего UI и нужным якорем.
- Английский и русский установщик показывают краткую Политику/Отказ до выбора каталога и требуют принять Условия на отдельном лицензионном экране.
- Ограничение ответственности явно сохраняет гарантии, подсудность и средства защиты, которые нельзя отменить договором.

## Проверенная фактическая обработка

Неизменённое приложение не содержит HTTP-клиента, SDK аналитики, рекламы, аккаунтов или облачного API. Имена и системные идентификаторы Bluetooth-устройств, адрес, класс, состояние подключения и заряд используются в памяти. На диск приложение записывает настройки и технические журналы с ротацией `5 × 512 КиБ`. Установщик выполняет локальные проверки prerequisites, ведёт локальный setup-log и открывает страницы Microsoft только после подтверждения.

Официальный сайт — https://bluetooth-notify.onrender.com/, репозиторий — https://github.com/vyach-vasiliev/bluetooth-notify/. Сайт размещён как Render Static Site. Его код не собирает данные; документация Render указывает, что статические сайты не создают журналы запросов приложения. Сам Render может обрабатывать сведения об устройстве/IP и инфраструктуре по своей политике конфиденциальности. Privacy/legal/support-контакт проекта — https://github.com/vyach-vasiliev/.

## Лицензия проекта

Исходный и объектный код предоставляются по GNU Affero General Public License версии 3 без автоматического перехода на последующие версии (`AGPL-3.0-only`). Лицензия разрешает в том числе коммерческое использование, изменение и распространение.

При распространении оригинального или изменённого covered work необходимо сохранить AGPL и предоставить Corresponding Source предусмотренным лицензией способом. Если изменённая версия поддерживает удалённое сетевое взаимодействие, раздел 13 AGPL требует предложить её пользователям бесплатный доступ к соответствующему исходному коду. AGPL 3.0 является одобренной OSI open-source лицензией.

## Региональное покрытие

| Язык/регион | Учтённая рамка | Реализованный подход |
| --- | --- | --- |
| `de`, `fr`, `es`, `pt` и пользователи ЕС/ЕЭЗ | GDPR, правила ЕС о несправедливых условиях и цифровом контенте | прозрачный перечень данных/целей/сроков/прав; обязательные права имеют преимущество |
| `en` / США, включая Калифорнию | FTC transparency/security guidance; CCPA/CPRA disclosures where applicable | раскрытие категорий за 12 месяцев; явно указано отсутствие продажи, sharing, профилирования и дискриминации |
| `ru` / Россия | Федеральный закон № 152-ФЗ, включая публичность политики и локализацию интернет-сбора | описана локальная обработка, отсутствие сетевого сбора приложением и контакт проекта |
| `ja` / Япония | APPI и материалы PPC Japan | цели, категории, безопасность, сроки, права и контакт оператора |
| `ko` / Республика Корея | PIPA, в частности публичная privacy policy | цели, категории, сроки, удаление, третьи лица и права |
| `zh-CN` / КНР | PIPL и обязательные права потребителей | минимизация, цели, срок, получатели, права, отсутствие трансграничной передачи приложением |

Официальные и первичные ориентиры:

- GDPR, статьи 5 и 13: https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:32016R0679
- European Commission, unfair contract terms: https://commission.europa.eu/law/law-topic/consumer-protection-law/consumer-contract-law/unfair-contract-terms-directive_en
- European Commission, digital contract rules: https://commission.europa.eu/topics/business-and-industry/contract-rules/digital-contracts/digital-contract-rules_en
- California Attorney General, CCPA: https://oag.ca.gov/privacy/ccpa
- FTC, security guidance for app developers: https://www.ftc.gov/business-guidance/resources/app-developers-start-security
- Федеральный закон № 152-ФЗ, статья 18.1: https://www.consultant.ru/document/cons_doc_LAW_61801/eeeebe22bf738fd65bb66b95cc278911ae2525ee/
- PPC Japan, APPI laws and policies: https://www.ppc.go.jp/en/legal/
- Korean National Law Information Center, PIPA: https://law.go.kr/LSW/lsInfoP.do?urlMode=engLsInfoR&viewCls=engLsInfoR&lsiSeq=248613
- КНР, Закон о защите персональной информации: https://www.npc.gov.cn/c2/c30834/202108/t20210820_313088.html
- GNU AGPL 3.0: https://www.gnu.org/licenses/agpl-3.0.html
- OSI, GNU AGPL 3.0: https://opensource.org/license/agpl-3.0
- Render Privacy Policy: https://render.com/privacy
- Render, logging (включая отсутствие application request logs у Static Sites): https://render.com/docs/logging
