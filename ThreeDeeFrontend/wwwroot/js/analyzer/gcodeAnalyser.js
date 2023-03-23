var gcodeProcessorWorker = new Worker('js/analyzer/gcodeProcessor.js');
var gcodeLines = undefined;
var selectedSettings = 0;
var renderType = 0;
var results = Array(4);
var resultFieldIds = [];
var currentCalculationSetting = 0;

 
gcodeProcessorWorker.onmessage = function (e) {
    if ("resultFormat" in e.data) {
        addResultTableEntries(e.data.resultFormat);
    } else if ("progress" in e.data) {
        setProgressBarPercent(e.data.progress);
    } else if ("complete" in e.data) {
        document.getElementById("progress").style = "display:none;";
        document.getElementById("calculateButton").style = "margin-bottom: 20px; display:true;";
        setProgressBarPercent(0);
    } else if ("result" in e.data) {
        results[currentCalculationSetting] = e.data.result;
        $("#layerNumber").text("(Generating...)");
        displayResult();
        generateView();
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

function setProgressBarPercent(percent) {
    var progressBar = document.getElementById("progressBar");
    progressBar.style = "-webkit-transition: none; transition: none;width: " + percent + "%;";
    progressBar.setAttribute("aria-valuenow", percent);
    progressBar.innerHTML = percent + "%";
}

function selectRenderType(newRenderType) {
    document.getElementById("selectRender" + renderType).className = "btn btn-primary";
    document.getElementById("selectRender" + newRenderType).className = "btn btn-primary active";
    renderType = newRenderType;
    setRenderType(renderType);
}

function displayProgressBar() {
    setProgressBarPercent(0);
    document.getElementById("progress").style = "margin-bottom: 14px; display:true;";
    document.getElementById("calculateButton").style = "display:none;";
}

function refreshStatistics() {
    if (gcodeLines != undefined) {
        displayProgressBar();
        gcodeProcessorWorker.postMessage([gcodeLines, simpleSettingsDict(selectedSettings)]);
        currentCalculationSetting = selectedSettings;
    }
}


function registerUpload(){
    const inputElement = document.getElementById("input");
    inputElement.addEventListener("change", readFile, false);
}


function readFile() {
    const fileList = this.files; /* now you can work with the file list */
    var f = fileList[0];
    if (f) {
        var size;
        if (f.size / 1024 / 1024 < 1) {
            size = (f.size / 1024).toFixed(1) + "KB";
        } else {
            size = (f.size / 1024 / 1024).toFixed(1) + "MB";
        }
        //dropzone.innerHTML = f.name + " - " + size;
        var r = new FileReader();
        r.onload = function (e) {
            gcodeLines = e.target.result.split(/\s*[\r\n]+\s*/g);
            refreshStatistics();
        }
        r.readAsText(f);
        displayProgressBar();
    }
}


var doit;


function onloadInit() {
    window.onresize = function () {
        clearTimeout(doit);
        doit = setTimeout(resizeCanvas, 100);
    };
    // Request Result Format
    gcodeProcessorWorker.postMessage("getResultFormat");
    // Printer Attribute
    $("#canvasVerticalSlider").slider({
        orientation: "vertical",
        min: 1,
        max: 2,
        step: 1,
        values: 1,
    });
    $("#canvasVerticalSlider").height($("#renderCanvas").height() - 96);
    $("#canvasVerticalSlider").on("slide", function (event, ui) { setRender(ui.value) });
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

    for (key in resultFormat) {
        if (resultFormat[key].table == "gcodeStatsPerAxis") {
            var table = document.getElementById("gcodeStatsPerAxis");
            var row = table.insertRow(-1);
            var cell = row.appendChild(document.createElement('th'));
            cell.innerHTML = resultFormat[key].discription;
            for (var i = 0; i < resultFormat[key].fieldId.length; i++) {
                cell = row.insertCell(-1);
                var span = document.createElement("span");
                span.id = resultFormat[key].fieldId[i];
                resultFieldIds.push(resultFormat[key].fieldId[i]);
                cell.appendChild(span);
            }
        }
    }
}
