pico-8 cartridge // http://www.pico-8.com
version 41
__lua__
function _init()
 
 size = 127
 generation = 1
	
	cellgrid = {}
	
	for row = 0, size do
	 cellgrid[row] = {}	 
	 for column = 0, size do	  

	  if flr(rnd(5)) == 1 then
	   cellgrid[row][column] = true
	  else
	   cellgrid[row][column] = false
	  end

	 end
	end
	
	
	nextgen = {}
	
	for row = 0, size do
	 nextgen[row] = {}	 
	 for column = 0, size do
	  
	  nextgen[row][column] = false
	  
	 end
	end
	
end


function _update()

	
	
end


function _draw()
	cls()
	
	calculatenextgen()
	updatecurrentgen()
	
	sspr(0,0,size,size)
end


function calculatenextgen()

	for x=0,size do
	 for y=0,size do
	   nextgen[x][y] = shouldbealive(x,y)
	 end
	end

end


function updatecurrentgen()

	for x=0,size do
	 for y=0,size do
	   cellgrid[x][y] = nextgen[x][y]
	   if cellgrid[x][y] then
	   sset(x,y,7)
	   else
	   	sset(x,y,0)
	   end
	 end
	end
	
end


function shouldbealive(x,y)
	
	local aliveneighbours = 0
	
	if cellgrid[wrap(x-1)][wrap(y+1)] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[x][wrap(y+1)] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[wrap(x+1)][wrap(y+1)] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[wrap(x-1)][y] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[wrap(x+1)][y] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[wrap(x-1)][wrap(y-1)] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[x][wrap(y-1)] then
		aliveneighbours = aliveneighbours +1 end

	if cellgrid[wrap(x+1)][wrap(y-1)] then
		aliveneighbours = aliveneighbours +1 end
 	
	if cellgrid[x][y] and aliveneighbours == 2
	 then	return true
	elseif cellgrid[x][y] and aliveneighbours == 3 then
		return true
	elseif cellgrid[x][y] == false and aliveneighbours == 3 then
		return true
	else 
		return false
 end
 
end


function wrap(i)

	if i > size then
		i = 0
	elseif i < 0 then
		i = size
	end
	
	return i

end
__gfx__
00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
00700700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
00077000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
00077000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
00700700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
