const {
  ClassicEditor: ClassicEditorBase,
} = require("@ckeditor/ckeditor5-editor-classic");
const {
  Bold,
  Italic,
  Underline,
  Strikethrough,
  Code,
  Subscript,
  Superscript,
} = require("@ckeditor/ckeditor5-basic-styles");
const { Autoformat } = require("@ckeditor/ckeditor5-autoformat");
const { TextTransformation } = require("@ckeditor/ckeditor5-typing");
const { Indent, IndentBlock } = require("@ckeditor/ckeditor5-indent");
const { BlockQuote } = require("@ckeditor/ckeditor5-block-quote");
const { Clipboard } = require("@ckeditor/ckeditor5-clipboard");
const { FindAndReplace } = require("@ckeditor/ckeditor5-find-and-replace");
const { Font } = require("@ckeditor/ckeditor5-font");
const { Heading } = require("@ckeditor/ckeditor5-heading");
const { Highlight } = require("@ckeditor/ckeditor5-highlight");
const {
  Image,
  ImageCaption,
  ImageResize,
  ImageStyle,
  ImageToolbar,
  ImageUpload,
  ImageInsert,
  AutoImage,
} = require("@ckeditor/ckeditor5-image");
const { SimpleUploadAdapter } = require("@ckeditor/ckeditor5-upload");
const { Link, LinkImage, AutoLink } = require("@ckeditor/ckeditor5-link");
const { List } = require("@ckeditor/ckeditor5-list");
const { MediaEmbed } = require("@ckeditor/ckeditor5-media-embed");
const { ShowBlocks } = require("@ckeditor/ckeditor5-show-blocks");
const { SpecialCharacters } = require("@ckeditor/ckeditor5-special-characters");
const {
  SpecialCharactersEssentials,
} = require("@ckeditor/ckeditor5-special-characters");

const { Paragraph } = require("@ckeditor/ckeditor5-paragraph");
const { Table, TableToolbar } = require("@ckeditor/ckeditor5-table");
const { Alignment } = require("@ckeditor/ckeditor5-alignment");
const { Essentials } = require("@ckeditor/ckeditor5-essentials");
const { PasteFromOffice } = require("@ckeditor/ckeditor5-paste-from-office");

class ClassicEditor extends ClassicEditorBase {}

ClassicEditor.builtinPlugins = [
  Bold,
  Italic,
  Underline,
  Strikethrough,
  Code,
  Subscript,
  Superscript,
  Autoformat,
  TextTransformation,
  Indent,
  IndentBlock,
  BlockQuote,
  FindAndReplace,
  Clipboard,
  Font,
  Heading,
  Highlight,
  Essentials,
  Image,
  ImageResize,
  ImageCaption,
  ImageStyle,
  ImageToolbar,
  ImageInsert,
  AutoImage,
  ImageUpload,
  SimpleUploadAdapter,
  LinkImage,
  Link,
  AutoLink,
  List,
  MediaEmbed,
  ShowBlocks,
  Paragraph,
  SpecialCharacters,
  SpecialCharactersEssentials,
  Table,
  TableToolbar,
  Alignment,
  PasteFromOffice,
];

ClassicEditor.defaultConfig = {
  toolbar: {
    items: [
      "undo",
      "redo",
      // Format
      "|",
      "heading",
      "fontSize",
      "fontColor",

      // Basic style
      "|",
      "bold",
      "italic",
      "underline",
      "strikethrough",
      "subscript",
      "superscript",

      // paragraph
      "|",
      "alignment",
      "bulletedList",
      "numberedList",
      "outdent",
      "indent",
      "|",

      // 'imageStyle:block',
      // 'imageStyle:side',
      // 'toggleImageCaption',
      // 'imageTextAlternative',
      // "uploadImage",
      // 'linkImage',
      "link",
      "specialCharacters",
      "insertTable",
      "insertImage",
      "mediaEmbed",
      "|",
      "showBlocks",
      "findAndReplace",
      "|",
      "fontBackgroundColor",
      "highlight",
      "code",
    ],
  },
  resizeOptions: [
    {
      name: "resizeImage:original",
      value: null,
      label: "Original",
    },
    {
      name: "resizeImage:40",
      value: "40",
      label: "40%",
    },
    {
      name: "resizeImage:60",
      value: "60",
      label: "60%",
    },
  ],
  image: {
    toolbar: [
      "resizeImage",
      "imageStyle:inline",
      "imageStyle:block",
      "imageStyle:side",
      "|",
      "toggleImageCaption",
      "imageTextAlternative",
      "|",
      "linkImage",
    ],
  },
  fontSize: {
    options: [
      14,
      15,
      "default",
      18,
      20,
      22,
      24,
      28,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      39,
      40,
    ],
    supportAllValues: true,
  },
  fontColor: {
    colors: [
      {
        color: "#005BAA",
        label: "Màu chính 1",
      },
      {
        color: "#F05A22",
        label: "Màu chính 2",
      },
      {
        color: "#6DCEF5",
        label: "Màu chính 3",
      },
      {
        color: "#FDB814",
        label: "Màu phụ 1",
      },
      {
        color: "#292663",
        label: "Màu phụ 2",
      },
      {
        color: "#ed1c24",
        label: "Màu phụ 3",
      },
    ],
    columns: 3,
  },
  list: {
    properties: {
      styles: true,
      startIndex: true,
      reversed: true,
    },
  },
  simpleUpload: {
    // Feature configuration.
    uploadUrl: `${import.meta.env.VITE_BASE_API_URL}File/UploadFromCk`,
    withCredentials: false,
  },
  language: "en",
};
