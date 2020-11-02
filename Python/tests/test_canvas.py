from rt.color import *
from rt.canvas import *

class TestCanvas(object):
    def test_createCanvas(self):
        c = Canvas(10, 20)
        black = Color(0, 0, 0)

        assert c.Width == 10
        assert c.Height == 20

        for i in range(c.Width):
            for j in range(c.Height):
                assert c[[i, j]] == black

    def test_canvasWrite(self):
        c = Canvas(10, 20)
        red = Color(1, 0, 0)
        c[[2, 3]] = red
        
        assert c[[2, 3]] == red

    def test_canvasHeader(self):
        c = Canvas(5, 3)
        ppm = c.GetPPM()
        lines = ppm.splitlines()

        assert lines[0] == "P3"
        assert lines[1] == "5 3"
        assert lines[2] == "255"

    def test_canvasNormal(self):
        c = Canvas(5, 3)
        c1 = Color(1.5, 0, 0)
        c2 = Color(0, 0.5, 0)
        c3 = Color(-0.5, 0, 1)

        c[[0, 0]] = c1
        c[[2, 1]] = c2
        c[[4, 2]] = c3

        ppm = c.GetPPM()
        lines = ppm.splitlines()

        assert lines[3] == "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0"
        assert lines[4] == "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0"
        assert lines[5] == "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255"

    def test_canvasLongLines(self):
        c = Canvas(10, 2)
        p = Color(1, 0.8, 0.6)

        for i in range(c.Width):
            for j in range(c.Height):
                c[[i, j]] = p

        ppm = c.GetPPM()
        lines = ppm.splitlines()

        assert lines[3] == "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204"
        assert lines[4] == "153 255 204 153 255 204 153 255 204 153 255 204 153"
        assert lines[5] == "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204"
        assert lines[6] == "153 255 204 153 255 204 153 255 204 153 255 204 153"

    def test_canvasEndsNewline(self):
        c = Canvas(5, 3)
        ppm = c.GetPPM()

        assert ppm[-1] == '\n'
