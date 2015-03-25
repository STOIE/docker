#!/bin/bash

mkdir $2/../tmp;
cd $2/../tmp;
cp $1 ./;
unzip ./*;
rm -f ./*.docx;

cd $2;
rsync -Pav -c ../tmp/ ./ --delete
git add .
git commit -m "We have an update..."
git push origin master
rm -rf ../tmp;
