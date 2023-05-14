import {Box} from "@mui/material";

import { useState} from "react";

import {useStyles} from "./styles";

export const ThemeMenu = () => {
  const {classes} = useStyles();

  const [open, setOpen] = useState({});

  const handleClick = (event: any) => {
    setOpen({[event.target.name]: !!event.target.value});
  };
  return (
    <Box className={classes.root}>
      {/*{items.list.map((list) => {*/}
      {/*  return (*/}
      {/*      <List className={classes.root} key={list.id} subheader={<ListSubheader>{list.title}</ListSubheader>}>*/}
      {/*        {list.items.map((item) => {*/}
      {/*          return (*/}

      {/*              <div key={item.id}>*/}
      {/*                {item.subitems != null ?  (*/}
      {/*                    <div key={item.id}>*/}
      {/*                      <ListItem button key={item.id} onClick={this.handleClick.bind(this, item.name)} >*/}
      {/*                        <ListItemText primary={item.name} />*/}
      {/*                        {this.state[item.name] ? <ExpandLess /> : <ExpandMore />}*/}
      {/*                      </ListItem>*/}
      {/*                      <Collapse key={list.items.id} component="li" in={this.state[item.name]} timeout="auto" unmountOnExit>*/}
      {/*                        <List disablePadding>*/}
      {/*                          {item.subitems.map((sitem) => {*/}
      {/*                            return (*/}
      {/*                                <ListItem button key={sitem.id} className={classes.nested}>*/}
      {/*                                  <ListItemText key={sitem.id} primary={sitem.name} />*/}
      {/*                                </ListItem>*/}
      {/*                            )*/}
      {/*                          })}*/}
      {/*                        </List>*/}
      {/*                      </Collapse> </div>*/}
      {/*                ) : (*/}
      {/*                    <ListItem button onClick={this.handleClickLink.bind(this, item.name)} key={item.id}>*/}
      {/*                      <ListItemText primary={item.name}  />*/}
      {/*                    </ListItem> )}*/}
      {/*              </div>*/}

      {/*          )*/}
      {/*        })}*/}
    </Box>
  );
};

