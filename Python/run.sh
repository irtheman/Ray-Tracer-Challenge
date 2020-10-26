echo Build...
python setup.py build

echo
echo Test...
pytest

echo
echo Main program...
python -m rt

echo
echo Projectile test...
python -m rt.projectile
