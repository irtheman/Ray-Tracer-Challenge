from setuptools import setup, find_packages

setup(
    name="rt_challenge",
    version="0.0.1",
    author="Matthew Hanna",
    author_email="matthew@matthewhanna.net",
    description="Ray Tracing Challenge In Python",
    packages=['rt', 'tests'],
    classifiers=[
        "Programming Language :: Python :: 3",
        "License :: OSI Approved :: Unspecified",
        "Operating System :: OS Independent",
    ],
    python_requires=">=3.8",
)
