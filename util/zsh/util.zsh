function _print_in_buffer() {
  if zle; then
    BUFFER=$1
    CURSOR=$#BUFFER
    zle clear-screen
  else
    print -z $1
  fi
}

