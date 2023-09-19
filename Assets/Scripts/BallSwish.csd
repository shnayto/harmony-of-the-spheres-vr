<Cabbage>
form caption("Untitled") size(400, 300), guiMode("queue") pluginId("def1")

button bounds(20, 18, 80, 40) channel("trigger")
hslider bounds(26, 62, 150, 50) channel("freq") range(0, 1000, 175, 1, 0.001)
</Cabbage>
<CsoundSynthesizer>
<CsOptions>
-n -d
</CsOptions>
<CsInstruments>
; Initialize the global variables. 
ksmps = 32
nchnls = 2
0dbfs = 1

;instr 1
;    kTrig chnget "trigger" 
;    if changed(kTrig) == 1 then 
;        event "i", "PlaySound", 0, 9
;    endif
;    printk2 kTrig   
;endin

instr 1
    kTrig chnget "trigger" 
    kFreq chnget "freq"
    aEnv expon 1000, p3, 0.00001
    aSig oscili 0.5, kFreq
    outs aSig, aSig 
     
endin

</CsInstruments>
<CsScore>
;causes Csound to run for about 7000 years...
f0 z
;starts instrument 1 and runs it for a week
i1 0 [60*60*24*7] 
</CsScore>
</CsoundSynthesizer>
