#!/usr/bin/perl


my $IMG_FOLDER = '/tmp/site/images/';
my $INFO_FOLDER = '/tmp/site/info/';
my $PATH_PREFIX = './images';
my $OUTPUT_DIR = '/tmp/site/';

my %people;
my $count = 1;


opendir (DIR, $IMG_FOLDER) or die $!;
while (my $file = readdir(DIR)) {

  if ($file =~ /^.*default.*$/) {
  } elsif ($file =~ /^(\S+)_(\d+)\.jpg$/) {
    my $wname = lc($1);
    my $cname = ucfirst($wname);
    $people{$cname}{$2} = '.jpg';
    #print "Name: " . $1 . "\nImg Num: " . $2 . "\nFull Path: " . $file . "\n\n";
  } elsif ($file =~ /^(\S+)_disp.jpg$/) {
    my $wname = lc($1);
    my $cname = ucfirst($wname);
    $people{$cname}{'disp'} = '.jpg';
  }

}
closedir(DIR);

opendir (DIR, $INFO_FOLDER) or die $!;
while (my $file = readdir(DIR)) {

  if ($file =~ /^.*default.*$/) {
  } elsif ($file =~ /^(\S+)\.info$/) {
    my $wname = lc($1);
    my $cname = ucfirst($wname);
    my $numpic = keys %{ $people{$cname} };
    #print $numpic . "\n";
    if ($numpic == 0) {
      $people{$cname}{'default'} = '.jpg';
    }
    $people{$cname}{'info'} = $cname;
  }

}
closedir(DIR);

my %ppl;

foreach my $person (sort keys %people) {
  #print $person . "\n";
  $count = 1;
  my $useas = 0;
  my $disp = 1;
  if ($people{$person}{'disp'} eq '') {
    $useas = 1;
  } else {
    #$ppl{$person}{'disp'} = lc($person) . '_disp.jpg';
    #print $person . " disp: " . lc($person) . '_disp.jpg' . "\n";
  }
  if ($people{$person}{'info'} ne '') {
    $ppl{$person}{'info'} = lc($person) . '.info';
    #print $person . " info: " . lc($person) . '.info' . "\n";
  } else {
    $ppl{$person}{'info'} = 'default.info';
    #print $person . " info: default" . '.info' . "\n";
  }
  foreach my $imgid (reverse sort keys %{ $people{$person} }) {
    if ($useas == 1 and $imgid ne 'info' and $imgid ne 'default') {
      #$ppl{$person}{'disp'} = lc($person) . '_' . $imgid . ".jpg";
      #print $person . " disp: " . lc($person) . '_' . $imgid . ".jpg\n";
      $useas = 0;
    } elsif ($useas == 1 and $imgid ne 'info' and $imgid eq 'default') {
      #$ppl{$person}{'disp'} = $imgid . ".jpg";
      #print $person . " disp: " . $imgid . ".jpg\n";
      $useas = 0;
    } elsif ($people{$person}{'disp'} ne '' and $disp == 1) {
      $ppl{$person}{"img$count"} = lc($person) . '_disp';
      #print $person . " - " . lc($person) . '_disp.jpg' . "\n";
      $disp = 0;
      $count++;
    }
    if ($count < 6 and $imgid ne 'disp' and $imgid ne 'info' and $imgid ne 'default') {
      $ppl{$person}{"img$count"} = lc($person) . '_' . $imgid;
      #print $person . " - " . lc($person) . '_' . $imgid . ".jpg\n";
      $count++;
    } elsif ($count < 6 and $imgid ne 'disp' and $imgid ne 'info' and $imgid eq 'default') {
      $ppl{$person}{"img$count"} = $imgid;
      #print $person . " - " . $imgid . ".jpg\n";
    }
  }
}


open (GAL, '> ' . $OUTPUT_DIR . './gallery.html');
print GAL "<html><head><title>Gallery</title></head><body><table>\n";
foreach my $person (sort keys %ppl) {
  print GAL "<tr><td colspan=5><a href=$PATH_PREFIX/$person.html>$person</a></td></tr>\n";
  print GAL "<tr><td colspan=3><a href=$PATH_PREFIX/$person.html><img src=$PATH_PREFIX/$ppl{$person}{'img1'}.jpg></a></td>\n";
  print GAL "<td colspan=2>$ppl{$person}{'info'}</td></tr><tr>\n";
  print GAL "</tr><tr><td height=50 colspan=5> </td></tr>\n";
}
print GAL "</table></body></html>\n";
close (GAL);


foreach my $person (sort keys %ppl) {
  open (PERZ, '> ' . $OUTPUT_DIR . "./${person}.html");
  print PERZ "<html><head><title>$person</title></head><body><table>\n";
  print PERZ "<tr><td colspan=5>$person</td></tr>\n";
  my $count = 1;
  foreach my $img (sort keys %{ $ppl{$person} }) {
    if ($img =~ /^img(\d)$/) {
      if ($1 == 1) {
        print PERZ "<tr><td colspan=3><img src=$PATH_PREFIX/$ppl{$person}{$img}.jpg></td>\n";
        print PERZ "<td colspan=2>$ppl{$person}{'info'}</td></tr><tr>\n";
        print PERZ "<td><a href=$PATH_PREFIX/$ppl{$person}{$img}.jpg><img src=$PATH_PREFIX/$ppl{$person}{$img}_thumb.jpg></a></td>\n";
      } else {
        print PERZ "<td><a href=$PATH_PREFIX/$ppl{$person}{$img}.jpg><img src=$PATH_PREFIX/$ppl{$person}{$img}_thumb.jpg></a></td>\n";
      }
      #print "$person $count = $ppl{$person}{$img}\n";
      $count++;
    }
  }
  while ($count < 6) {
    print PERZ "<td><img src=$PATH_PREFIX/default_thumb.jpg></td>\n";
    #print "$person $count = default.jpg\n";
    $count++;
  }
  print PERZ "</tr></table></body></html>\n";
  close (PERZ);
}
