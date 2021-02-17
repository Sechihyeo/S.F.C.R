create user sfc@localhost;
create schema sfc;
grant all privileges on sfc.* to sfc@localhost;
use sfc;

create table user (
  id varchar(30) not null primary key,
  k varchar(10),
  d varchar(10)
);