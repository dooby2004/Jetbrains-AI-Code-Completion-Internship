# Jetbrains-AI-Code-Completion-Internship
All code written for the project is in the base folder. All other code in /Data is only used to get samples from.

## Resulting dataset (Each prediction, followed by label)
```
api.py-0.pkl:
    Prediction - ion/vnd.openxmlformats-officedocument.wordprocessingml.document",
    Actual     - ion/msword",
```
Incorrect prediction, however, I did not expect it to predict data that is stored in the code (this is a list of file types)


```
api.py-1.pkl:
    Prediction - _TYPES = ["text/plain"]

ALLOWED_EXTENSIONS = set(["txt", "pdf"])

app = FastAPI()

# @app.on_event("startup")
# async def startup():
#     print("Starting Updater...")
#     await DBService.instance().init_db()

# @app.on_event("shutdown")
# async def shutdown():
#     print("Shutting Down...")
#     await DBService.instance().close_db()

# @app.exception_handler(HTTPException)
# as
    Actual     - _TYPES = [
```
Correct prediction, over-predicted


```
api.py-2.pkl:
    Prediction - _time = datetime.now()
    Actual     - _time = datetime.now().time()
```
Mostly correct prediction, this may have been valid depending on how this is later used


```
api.py-3.pkl:
    Prediction - on

    Actual     - on, UploadFile
```
Correct prediction, under-predicted


```
api.py-4.pkl:
    Prediction - t.wordprocessingml.document",
    Actual     - t.wordprocessingml.document",
```
Correct prediction


```
Form.cs-0.pkl:
    Prediction - ct sender, EventArgs e)
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-8.0
            using (OpenFileDialog keywordsOpen = new OpenFileDialog())
    Actual     - ct sender, EventArgs e)
```
Correct prediction, over-predicted


```
Form.cs-1.pkl:
    Prediction -  {
                altDecryptOutput.Update();
                try
                {
                    (Int32 code, string result) = MD5Methods.DecryptFromFile(altDecryptInput.Text, File.OpenRead(keywordsPath.Text), MD5Methods.EncryptMD5);
                    switch (code)
                    {
                        case 0:
                            altDecryptOutput.Text = result;
                            break;
                        case 1:
                            altDecryptOutput.Text = "No Solution Found";
                            break;
                        case 2:
                            altDecryptOutput.Text = "Invalid Hash";
                            break;
                    }
                }
            }
            catch (Argumen
    Actual     - DecryptOutput.Update();
```
Almost correct prediction, valid suggestion of function, and over-prediction


```
Form.cs-2.pkl:
    Prediction - yKeywordsPath.Text = keywordsOpen.FileName;
    Actual     - ywordsPath.Text = keywordsOpen.FileName;
```
Almost correct prediction, would not compile in context


```
Form.cs-3.pkl:
    Prediction - ing.Threading;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Stacks;
using System.Threading.Thread;
using System.Threading.ThreadPool;
using System.Threading.FileSystem;
using System.Threading.Process;
using System.Threading.Timer;
using Sys
    Actual     - em.Windows.Forms;
```
Incorrect prediction


```
Form.cs-4.pkl:
    Prediction - t ex)
    Actual     - tException ex)
```
Incorrect prediction


```
ImageProcessing.cs-0.pkl:
    Prediction - t System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
    Actual     - ng System.Linq;
```
Incorrect prediction, may have been confused by surrounding code


```
ImageProcessing.cs-1.pkl:
    Prediction -  = 0;
            int right = 0;
            int bottom = 0;
            int xStart = 0;
            int yStart = 0;
            int xEnd = bmp.Width - 1;
            int yEnd = bmp.Height - 1;
            int xStep = 1;
            int yStep = 1;
            int xOffset = 0;
            int yOffset = 0;
            int xCount = 0;
            int yCount = 0;
            int xCountLeft = 0;
            int yCountLeft = 0;
         
    Actual     -  pixelSize = 1;
```
Incorrect prediction


```
ImageProcessing.cs-2.pkl:
    Prediction - .ToArgb().ToString("RGB"));
            Debug.WriteLine(bmp.ConvertToGrayscale()).ToString("YCbCr");
            Debug.WriteLine(bmp.ConvertToRGBA()).ToString("BGRA");
            Debug.WriteLine(bmp.ConvertToHSV()).ToString("HSV");
            Debug.WriteLine(bmp.ConvertToYUV()).ToString("YUV");
            Debug.WriteLine(bmp.ConvertToAlpha()).ToString("Alpha");
            Debug.WriteLine(bmp.ConvertToHue()).ToString("Hue");
            Debug.WriteLine(bmp.ConvertToSaturation()).ToString("Saturation");
            Debug.WriteLine(bmp.ConvertToValue(
    Actual     - .ToArgb().ToString("X8"));
```
Almost correct prediction, would not expect parameters to be predicted


```
ImageProcessing.cs-3.pkl:
    Prediction - ound = bmp.ConvertToBitmap(img);
            }
            bool isGrayscale = false;
            bool isColor = false;
    Actual     - p = new Bitmap(img);
```
Incorrect variable prediction, correct intent


```
ImageProcessing.cs-4.pkl:
    Prediction - eturn bmp
    Actual     - eturn bmp;
```
Correct prediction


```
MD5Methods.cs-0.pkl:
    Prediction - Array.Copy(BitConverter.GetBytes(a), 0, results, 0, 4);
    Actual     - new byte[16];
```
Incorrect prediction


```
MD5Methods.cs-1.pkl:
    Prediction - urn (x ^ y ^ z);
        }
    Actual     - urn (x & y) | (~x & z);
```
Incorrect prediction


```
MD5Methods.cs-2.pkl:
    Prediction - s.Windows.Forms;
using System.IO.Serialization;
using System.IO.Marshal;
using System.IO.Memory;
using System.IO.Compression;
using System.IO.Data;
using System.IO.Xml;
using System.IO.Xml.XPath;
using System.IO.Xml.XPath.Attribute;
using System.IO.Xml.XPath.Element;
using System.IO.Xml.XPath.Element.Attribute;
using System.IO.Xml.XPath.Element.Element;
using System.IO.Xml.X
    Actual     - stem.Diagnostics;
```
Incorrect prediction


```
MD5Methods.cs-3.pkl:
    Prediction - , 0x8000000, 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x0000
    Actual     - 0, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
```
Incorrect prediction, again, would not expect data to be predicted


```
MD5Methods.cs-4.pkl:
    Prediction - 
    Actual     - Encrypt(test, hashMethod) == hash)
```
Incorrect prediction


```
QRMethods.cs-0.pkl:
    Prediction - end((bool)a[i]);
    Actual     - end((bool)a[i]);
```
Correct prediction


```
QRMethods.cs-1.pkl:
    Prediction -  6; i++)
    Actual     -  length; i++)
```
Incorrect prediction


```
QRMethods.cs-2.pkl:
    Prediction - ds(dataBuild, 0, 0, 0, 0);
            bool[] fullDataBools = dataBuild.Concatenate(CombineCols(nulled[4 * i].Reverse().ToArray(), nulled[4 * i + 1].Reverse().ToArray()));
            bool[] dataBools = new bool[8 * byteLength];
            Array.Copy(fullDataBools, dataBools, 8 * byteLength);
    Actual     - d.ToArray();
```
Incorrect prediction


```
QRMethods.cs-3.pkl:
    Prediction - = "{http://www.w3.org/XML/1998/namespace}"
"""

import sys
from typing import List, Optional, Union

if sys.version_info >= (3, 8):
    from io import BytesIO as StringIO
else:
    from io import StringIO


def to_bytes(value: str) -> bytes:
    """Convert value to bytes."""
    return value.encode('utf-8')


class XMLParser:

    def __init__(self, xml_string: str) -> None:
        self._xml_
    Actual     - Modux_QRCodes
```
Incorrect Prediction


```
QRMethods.cs-4.pkl:
    Prediction - rrors = new bool[15];
            input.CopyTo(bools, 0);
            return bools;
        }

        public static bool [] ToBools(BitArray input)
    Actual     - rrorsBuild = [];
```
Incorrect Prediction


## Metrics
**Exact Match**: 0.08

**CHRF**: 0.22406770288944244

**Prefix Min. Match**: 0.35779608498917964

*Longest matching prefix/min of reference and prediction lengths*

**Prefix Actual Match**: 0.31612259755707145

*Longest matching prefix/reference length*

**Substring Search**: 0.5613801375793175

*Longest matching substring/reference length*

**Min. 5 Substring Search**: 0.5053835851121768

*Same as Substring Search, but for substrings of length<5, the prediction, reference pair has correlation 0*


I believe that the best correlating metric from the above is the 'Prefix Min. Match', as it accounts for the model over and under-predicting, where underprediction is fine, given it will continue to predict beyond that point once the current prediction is accepted or rejected. It also ensures that the part that matches is the section of code immediately at the cursor, which is what we want to predict, rather than anything further along, like the Substring Search and CHRF metrics consider.

The only issue is overprediction in general which this model clearly does more than necessary, but depending on the goal, if we are looking at single-line completions, then we can simply take the first line of the prediction to be our completion instead. A metric on the current model would correlate badly in most cases due to this issue.

All of these metrics will not, however, understand the purpose of the suggested code completion, so different formulations of the same program are not accounted for, which is difficult to assess. Realtime syntax checks could also avoid some unfortunate suggestions that would not run or compile properly.

A metric based around edit-distance could also have been quite effective if the model was good at giving single-line predictions, to directly compare and avoid single character differences (such as using ' over ") from having much of an impact.

## Report
Throughout this investigation, I have considered the efficacy of this model only in the first line or 2 that is has suggested. A multiline completion analysis of this model would be more difficult and definitely require prompting of the desired outcome of the program, so given only surround lines and the beginning of the current line, I assumed any model should only accurately complete the rest of the line. Inclusions and other minor details to avoid errors are much easier to program manually by considering the keywords used in prediction, so this is not much of an issue when considering the completions themselves.

I saw that the model often likes to overpredict in a large portion of its answers, which it can't easily do correctly given the lack of information on the meaning of the code, so in actual use, I would only want to consider the first part of its prediction anyway, which is why I evaluated it based only on how close it was to achieving the same result as the actual line I was expecting (even if its prediction did more than that).

I found that the model performed much better on the smaller examples, in the first 3 files (first 15 samples) than in the latter 2 which were 3-5 times larger. It may have performed better if it was only fed small excerpts of the larger files so that it had a decent sized sample to work off of around the current line, but was not confused by far off parts of code that have less impact on what should be written next.
The model also, unsurprisingly, attempted to, but was terrible at, predicting data that is stored within the code, so a method of detecting when data is being input and preventing suggestions from being generated may be very useful.


I have learnt a lot from this investigation, and believe that if a single-line completion is desired as I looked at here, then a specialised model should likely be trained for that with restrictions in place to allow it to become better at predicting such things, as it is a slightly different requirement to be forced not to fill in ahead and only predict the next few words as accurately as possible.
