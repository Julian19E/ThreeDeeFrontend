var gcodeProcessorWorker = new Worker('js/analyzer/gcodeProcessor.js');
var doit;
var gcodeLines = undefined;
var selectedSettings = 0;
var results = Array(4);
var resultFieldIds = [];
var currentCalculationSetting = 0;

 
gcodeProcessorWorker.onmessage = function (e) {
    if ("resultFormat" in e.data) {
        addResultTableEntries(e.data.resultFormat);
    } else if ("result" in e.data) {
        results[currentCalculationSetting] = e.data.result;
        $("#layerNumber").text("(Generating...)");
        displayResult();
        initCanvas();
    } else if ("layers" in e.data) {
        gcodeProcessorWorker.postMessage("cleanup");
        initRender(e.data.layers, settingSets[currentCalculationSetting].filamentDiameter.value[0]);
    }
}

function displayResult() {
    for (var i = 0; i < resultFieldIds.length; i++) {
        var key = resultFieldIds[i];
        if (results[selectedSettings] != undefined && results[selectedSettings][key] != undefined) {
            document.getElementById(key).innerHTML = results[selectedSettings][key];
        } else {
            document.getElementById(key).innerHTML = "";
        }
    }
}

function registerUpload(){
    const inputElement = document.getElementById("input");
    inputElement.addEventListener("change", readFile, false);
}

function readFile() {
    const fileList = this.files; 
    var f = fileList[0];
    if (f) {
        var r = new FileReader();
        r.onload = function (e) {
            gcodeLines = e.target.result.split(/\s*[\r\n]+\s*/g);
            if (gcodeLines != undefined) {
                gcodeProcessorWorker.postMessage([gcodeLines, simpleSettingsDict(selectedSettings)]);
                currentCalculationSetting = selectedSettings;
            }
        }
        r.readAsText(f);
    }
}

var slider  = undefined;
function onloadInit() {
    slider = document.getElementById("myRange");
    slider.min = 1;
    slider.max = 2;
    slider.step = 1;
    window.onresize = function () {
        clearTimeout(doit);
        doit = setTimeout(resizeCanvas, 100);
    };
    gcodeProcessorWorker.postMessage("getResultFormat");
    /*$("#canvasVerticalSlider").slider({
        orientation: "vertical",
        min: 1,
        max: 2,
        step: 1,
        values: 1,
    });*/
    //$("#canvasVerticalSlider").height($("#renderCanvas").height() - 96);
    slider.oninput = function() {
        setRender(this.value)
    }
    //$("#canvasVerticalSlider").on("slide", function (event, ui) { setRender(ui.value) });
    loadSettings();
    registerUpload();
    initCanvas();
}

function addResultTableEntries(resultFormat) {
    var gcodeStatsCount = 0;
    for (key in resultFormat) {
        if (resultFormat[key].table == "gcodeStats") {
            gcodeStatsCount++;
        }
    }
    var maxRow = Math.ceil(gcodeStatsCount / 2);
    var gcodeStatsTableContent = [Array(maxRow), Array(maxRow)];
    var row = 0;
    var col = 0;
    for (key in resultFormat) {
        if (resultFormat[key].table == "gcodeStats") {
            if (!key.startsWith("Category")) {
                resultFieldIds.push(key);
            }
            gcodeStatsTableContent[col][row] = key;
        }
        row++;
        if (row == maxRow) {
            row = 0;
            col++;
        }
    }

    for (var i = 0; i < maxRow; i++) {
        var table = document.getElementById("gcodeStats");
        var row = table.insertRow(-1);
        for (var j = 0; j < 2; j++) {
            var key = gcodeStatsTableContent[j][i];
            if (key == undefined) {
                var cell = row.appendChild(document.createElement('th'));
                cell.setAttribute("colspan", 2);
                break;
            }
            var cell = row.appendChild(document.createElement('th'));
            cell.innerHTML = resultFormat[key].discription;
            if (key.startsWith("Category")) {
                cell.className = "bg-info text-center col-xs-6 col-sm-6 col-md-6 col-lg-6";
                cell.setAttribute("colspan", 2);
            } else {
                cell.className = "col-xs-3 col-sm-3 col-md-3 col-lg-3";
                cell = row.insertCell(-1);
                cell.className = "col-xs-3 col-sm-3 col-md-3 col-lg-3";
                var span = document.createElement("span");
                span.id = key;
                cell.appendChild(span);
            }
        }
    }    
}
