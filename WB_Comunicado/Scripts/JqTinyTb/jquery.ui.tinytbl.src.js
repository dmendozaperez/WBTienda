/**
 * jQuery UI TinyTbl
 * Creates a scrollable table with fixed thead, tfoot and columns
 *
 * Copyright (c) 2011 Michael Keck <http://www.michaelkeck.de/>
 * Released:  2011-10-08
 * Version:   2011-10-08
 * I-Version: 1.9.0a
 * License:   Dual licensed under the MIT or GPL Version 2 licenses.
 *            http://jquery.org/license
 * Depends:   jquery.ui.core
 *            jquery.ui.widget
 */
(function($) {

    $.widget('ui.tinytbl', {

        options: {
            cols:0, direction:'ltr',
            tbodycss:'ui-widget-content',
            tfoot:true, tfootcss:'ui-widget-header',
            thead:true, theadcss:'ui-widget-header',
            height:'auto', width:'auto', focus: false
        },


        /**
         * pre init function
         */
        _init: function() {
            var u = 'undefined', t = this, iv = function(v) { if (typeof(v) !== u && !isNaN(parseInt(v, 10))) { return parseInt(v,10); } return false; };
            if (!(typeof(t.csn) !== u && t.csn)) {
                d = 'ui-tinytbl', f = '-first', l = '-last', r = 'row', x = 'col';
                t.csn = {
                    tbl:  d,       /* className for table-wrap */
                    tb:   d+'-tb', /* className for tbody-wrap */
                    tf:   d+'-tf', /* className for tfoot-wrap */
                    th:   d+'-th', /* className for thead-wrap */
                    colb: d+f+x,   /* className for first cell in table-row */
                    cole: d+l+x,   /* className for last cell in table-row */
                    rowb: d+f+r,   /* className for first row in any table */
                    rowe: d+l+r    /* className for last row in any table */
                };
            }
            /* get scrollbar width/height */
            if (!iv(t.sbw)) {
                var a = 0, b = 0, c = $('<div style="width:50px;height:50px;overflow:hidden;position:absolute;top:-80px;left:0px"><div style="height:80px"></div>');
                $('body').append(c);
                a = $('div', c).innerWidth();
                c.css({'overflow-y':'scroll'});
                b = $('div', c).innerWidth();
                $(c).remove();
                t.sbw = iv(a)-iv(b);
                t.sbf = Math.round(t.sbw*.5);
            }
        },

        /**
         * add a new row to the table body
         */
        _ra: function(b, a) {
            var t = this, d = t.element.data(), o = d.opt, s = 0, tb = 'tbody', tc = 'td,th';
            var lc = a.clone(), lt = $(tb,d.tb3), rc = a.clone(), rt = $(tb,d.tb4);
            $(tc,rc).each(function(x) { if (x < o.cols) { $(this).remove(); } });
            $(tc,lc).each(function(x) { if (x > (o.cols)-1) { $(this).remove(); } });
            if (b !== 'prepend') {
                lt.append(lc);
                rt.append(rc);
                s = 1;
            } else {
                lt.prepend(lc);
                rt.prepand(rc);
            }
            t._rs(tb);
            t._sr(tb);
            d.tb2.scrollTop((s > 0) ? d.size.hr : 0);
        },

        /**
         * remove a row from the table body
         */
        _rd: function(a) {
            var t = this, d = t.element.data(), tb = 'tbody';
            $(a, $(tb,d.tb3)).remove();
            $(a, $(tb,d.tb4)).remove();
            t._rs(tb);
        },

        /**
         * Applies some specials css-styles to the rows
         */
        _rs: function(a) {
            var t = this, c = t.csn, d = t.element.data(), o = d.opt, l, r, f = function(x) {
                var y = $('tr', x), tf = (($('tfoot', x).size() > 0 && !o.tf) ? true : false), th =  (($('thead', x).size() > 0 && !o.th) ? true : false);
                if (y.length < 1) {
                    return;
                }
                y.each(function(){ $(this).removeClass(c.rowb).removeClass(c.rowe); });
                if (!th && !tf) {
                    y.first().addClass(c.rowb);
                    y.last().addClass(c.rowe);
                } else {
                    if (!th) {
                        $('tbody tr', x).first().addClass(c.rowb);
                    }
                    if (tf) {
                        $('thead tr', x).last().addClass(c.rowe);
                    } else {
                        $('tbody tr', x).last().addClass(c.rowe);
                    }
                }
                y.each(function() {
                    var b = $('td,th',this);
                    $(b).removeClass(c.colb).removeClass(c.cole);
                    $(b[0]).addClass(c.colb);
                    $(b[b.length-1]).addClass(c.cole);
                });
            };
            if (!a) {
                a = 'tbody';
            }
            a = (''+a).toLowerCase();
            switch(a) {
                case 'tfoot':
                case 'foot':
                    l = d.tf3; r = d.tf4;
                    break;
                case 'thead':
                case 'head':
                    l = d.th3; r = d.th4;
                    break;
                default:
                    l = d.tb3; r = d.tb4;
                    break;
            }
            f(l); f(r);
        },

        /**
         * set the columns for each row in the left (fixed)
         * and right (scrollable) area
         */
        _sc: function(a) {
            var t = this, c = t.csn, d = t.element.data(), o = d.opt, l, r;
            if (!a) {
                a = 'tbody';
            }
            a = (''+a).toLowerCase();
            switch(a) {
                case 'tfoot':
                case 'foot':
                    l = d.tf3; r = d.tf4;
                    break;
                case 'thead':
                case 'head':
                    l = d.th3; r = d.th4;
                    break;
                default:
                    l = d.tb3; r = d.tb4;
                    break;
            }
            $('tr',r).each(function(x) {
                y = $('td,th',this);
                y.each(function(z) { if (z < o.cols) { $(this).remove(); } });
            });
            $('tr',l).each(function(x) {
                y = $('td,th',this);
                y.each(function(z) { if (z > (o.cols)-1) { $(this).remove(); } });
            });
            t._rs(a);
        },

        /**
         * syncs the cell height for each row in the left (fixed)
         * and right (scrollable) area
         */
        _sr: function(a) {
            var e = this.element, d = e.data(), o = d.opt, i= j = 0,x = 'td,th', y = 'tr';
            if (!o.cols) {
                return;
            }
            if (!a) {
                a = 'tbody';
            }
            a = (''+a).toLowerCase();
            switch(a) {
                case 'tfoot':
                case 'foot':
                    l = d.tf3; r = d.tf4;
                    break;
                case 'thead':
                case 'head':
                    l = d.th3; r = d.th4;
                    break;
                default:
                    l = d.tb3; r = d.tb4;
                    break;
            }
            var h = { l:[], r:[] };
            $(y,r).each(function(i) { h.l[i] = $(this).first(x).outerHeight(); });
            $(y,l).each(function(i) { h.r[i] = $(this).first(x).outerHeight(); });
            $(y,r).each(function(i) { j = h.r[i]; if (h.l[i] > h.r[i]) { j = h.l[i]; }; $(x, this).first().css({'height':j+'px'}); });
            $(y,l).each(function(i) { j = h.l[i]; if (h.r[i] > h.l[i]) { j = h.r[i]; }; $(x, this).first().css({'height':j+'px'}); });
            e.data({size:$.extend(d.size, {hl:l.height(),hr:r.height()})});
        },

        /**
         * creates the table header, table footer, table body and the
         * left (fixed) and right (scrollable) area
         */
        _tc: function(a) {
            if (!a) {
                a = 'tbody';
            }
            a = (''+a).toLowerCase();
            var b = 0, t = this, d = t.element.data(), o = d.opt, h, l, r, z = 'table', g = 'colgroup';
            var _attr = function(a) {
                var b = 'table';
                $(b,a).attr('cellpadding',d.padding);
                $(b,a).attr('cellspacing',d.spacing);
                $(b,a).attr('border','0');
            };
            switch (a) {
                case 'tfoot':
                case 'foot':
                    if (o.tf) {
                        a = 'tfoot'; b = o.tf;
                        h = '<'+z+'><'+a+(b.id ? ' role="'+b.id+'"' : '')+(b.csn ? ' class="'+b.csn+'"' : '')+'>'+$(a,d.cln).html()+'</'+a+'></'+z+'>';
                        l = d.tf3; r = d.tf4;
                    }
                    break;
                case 'thead':
                case 'head':
                    if (o.th) {
                        a = 'thead'; b = o.th;
                        h = '<'+z+'><'+a+(b.id ? ' role="'+b.id+'"' : '')+(b.csn ? ' class="'+b.csn+'"' : '')+'>'+$(a,d.cln).html()+'</'+a+'></'+z+'>';
                        l = d.th3; r = d.th4;
                    }
                    break;
                default:
                    a = 'tbody';
                    l = d.tb3; r = d.tb4;
                    break;
            }
            if (a !== 'tbody') {
                if (b) {
                    r.append(h);
                    if (o.cols) {
                        l.append(h);
                        $(g,l).remove();
                        $(z,l).prepend(d.size.cl).css({'width':d.size.wl+'px'});
                        _attr(l);
                        l.css({'width':d.size.wl+'px'});
                    }
                    t._sc(a);
                    $(g,r).remove();
                    $(z,r).prepend(d.size.cr).css({'width':d.size.wr+'px'});
                    _attr(r);
                    r.css({'width':d.size.wr+'px'});
                    this._sr(a);
                }
            } else {
                if (o.cols) {
                    l.append(r.html());
                    $(g,l).remove();
                    $(z,l).prepend(d.size.cl).css({'width':d.size.wl+'px'});
                    if ($(a,l).attr('id')) {
                        $(a,l).attr('role', $(a,l).attr('id') || '').removeAttr('id');
                    }
                    _attr(l);
                    l.css({'width':d.size.wl+'px'});
                }
                t._sc(a);
                $(g,r).remove();
                $(z,r).prepend(d.size.cr).css({'width':d.size.wr+'px'});
                if ($(a,r).attr('id')) {
                    $(a,r).attr('role', $(a,r).attr('id') || '').removeAttr('id');
                }
                _attr(r);
                r.css({'width':d.size.wr+'px'});
                t._sr(a);
            }
        },

        /**
         * Set the dimensions (width/height) of TinyTBL
         */
        _td: function() {
            var t= this, d = t.element.data(), o = d.opt;
            s = { hl:0, hr:(o.height - d.th2.height() - d.tf2.height()), ws:(o.width - d.tb1.width()-1), wf:0 };
            if (s.hr < 100) { s.hr = 100; }
            s.hl = s.hr;
            if (s.ws < 100) { s.ws = 100; }
            s.wf = s.ws;
            if (d.tb2.width() > s.ws) { s.hl = s.hl - t.sbw; }
            if (d.tb2.height() > s.hr) { s.wf = s.wf - t.sbw; }
            d.tbl.css({'width':(o.width)+'px','height':(o.height)+'px'});
            d.th2.css({'width':s.wf+'px' });
            d.tf2.css({'width':s.wf+'px' });
            d.tb1.css({'height':s.hl+'px' });
            d.tb2.css({'width':s.ws+'px','height':s.hr+'px' });
        },

        /**
         * internal function called via _create
         */
        _ti: function() {
            var t = this, d = t.element.data(), o = d.opt, a, z = 'table';
            d.tb4.append('<'+z+'>'+d.cln.html()+'</'+z+'>');
            if (o.tf) {
                a = 'tfoot';
                $(a,d.tb4).remove();
                t._tc(a);
            }
            if (o.th) {
                a = 'thead';
                $(a,d.tb4).remove();
                t._tc(a);
            }
            t._tc('tbody');
            $(z,d.tbl).each(function() { $(this).css({'border-collapse':'collapse','table-layout':'fixed'}); });
        },

        /**
         * syncs the scroll position
         */
        _ts: function(a) {
            var t = this, o = t.element.data();
            if (!o.tb2) { return; }
            var x = o.tb2.scrollLeft(), y = o.tb2.scrollTop();
            if (o.tb1) { o.tb1.scrollTop(y); }
            if (o.th2) { o.th2.scrollLeft(x); }
            if (o.tf2) { o.tf2.scrollLeft(x); }
        },

        /**
         * creates (inits) the TinyTBL
         */
        _create: function() {
            var bd = document, bw = window, a, c, v, w, d, e, k, o, p, s = {}, t = this, tc, tr, x = {}, nv = function(v) {
                v = (''+v).replace(/[^0-9\.]/gi, '');
                if (!isNaN(parseFloat(v))) {
                    return parseFloat(v);
                } else {
                    return 0;
                }
            }, hp = function(v) {
                if (v.parent().get(0).tagName !== 'body' && v.parent().get(0).tagName !== 'html') {
                    return true
                }
                return false;
            }, sv = function(v) {
                if (v === 'auto' || (''+v).lastIndexOf('%') !== -1) {
                    return true;
                }
                return false;
            };
            e = t.element;

            /* Extend options */
            o = t.options;
            o.direction = (''+o.direction).substring(0, 1).toLowerCase();
            if (o.direction == 'r') {
                o.rtl = true;
            } else {
                o.direction = '';
                o.rtl = false;
            }
            if ((''+o.width).lastIndexOf('%') !== -1 && nv(o.width) >= 100) {
                o.width = 'auto';
            }
            if ((''+o.height).lastIndexOf('%') !== -1 && nv(o.height) >= 100) {
                o.height = 'auto';
            }
            var pa = $('body'), ph = (bw.innerHeight || self.innerHeight || (bd.documentElement && bd.documentElement.clientWidth) || bd.body.clientWidth), pw = (bw.innerWidth || self.innerWidth || (bd.documentElement && bd.documentElement.clientWidth) || bd.body.clientWidth);

            /* Internal width and heights */
            x = { size: { cl:'', cr:'', wl:0, wr:0, hl:0, hr:0 } };

            /* only do it, if we have <table>-tag */
            if ((''+e.get(0).tagName).toLowerCase() !== 'table') {
                return;
            }

            /* get table rows */
            tr = $('tr',e);
            if (tr.length < 1) {
                /* no rows? exit! */
                return;
            }

            /* get table cols */
            tc = $('th,td',tr[0]);
            if (tc.length < 1) {
                /* no cols? exit! */
                return;
            }

            /* check for thead, tfoot and tbody */
            if ($('tbody',e).size() > 0) {
                if (o.tfoot) {
                    a = 'tfoot';
                    if ($(a,e).size() < 1) {
                        o.tf = false;
                    } else {
                        if (o.tfoot) {
                            o.tf = { 'id':($(a,e).attr('id') || 0), 'csn':($(a,e).attr('class') || 0) };
                        }
                    }
                }
                if (o.thead) {
                    a = 'thead';
                    if ($(a,e).size() < 1) {
                        o.th = false;
                    } else {
                        if (o.thead) {
                            o.th = {'id':($(a,e).attr('id') || 0), 'csn':($(a,e).attr('class') || 0) };
                        }
                    }
                }
            } else {
                o.tf = false; o.th = false;
            }

            v = 0;
            if (o.cols) {
                v = nv(o.cols);
            }
            if (v > 0 && v < tc.length) {
                o.fixed = (v-1);
            } else {
                o.fixed = -1; o.cols = false;
            }

            /* no thead, no tfoot and no fixed cols? exit! */
            if (!o.th && !o.tf && !o.cols) {
                return false;
            }

            /* get the width of each col */
            s = { c:'<col style="width:{w}px" width="{w}" />', g:'colgroup', l:'', r:'' };
            if (o.cols) {
                tc.each(function(i) {
                    w = $(this).outerWidth();
                    if (i > o.fixed) {
                        x.size.wr += w;
                        s.r += (s.c).replace(/{w}/g, w);
                    } else {
                        x.size.wl += w;
                        s.l += (s.c).replace(/{w}/g, w);
                    }
                });
            } else {
                tc.each(function(i) {
                    w = $(this).outerWidth();
                    x.size.wr += w;
                    s.r += (s.c).replace(/{w}/g, w);
                });
            }
            x.size.cl = ((s.l !== '') ? '<'+s.g+'>'+s.l+'</'+s.g+'>' : '');
            x.size.cr = ((s.r !== '') ? '<'+s.g+'>'+s.r+'</'+s.g+'>' : '');

            /* check for table attributes */
            x.padding = (e.attr('cellpadding') || '0');
            x.spacing = (e.attr('cellspacing') || '0');

            a = { a:'auto', m:'margin', p:'padding', b:'border', w:'Width', t:'Top', f:'Bottom', l:'Left', r:'Rigth' };

            /* call the init function to apply classNames and some settings */
            t._init();
            v = 0;
            s = 'visible';
            if (sv(o.height)) {
                v = ph;
                if (hp(e)) {
                    pa = e.parent();
                    v = pa.height();
                    if (o.height == a.a) {
                        if (!(pa.css('overflow') == s || pa.css('overflowY') == s)) {
                            v = v - t.sbf;
                        }
                    }
                }
                if (o.height !== a.a) {
                    v = Math.floor((v*0.01)*nv(o.height));
                }
                v = v-(nv(pa.css(a.m+a.t))+nv(pa.css(a.m+a.f))+nv(pa.css(a.p+a.t))+nv(pa.css(a.p+a.f))+nv(pa.css(a.b+a.t+a.w))+nv(pa.css(a.b+a.f+a.w)));
            } else {
                v = nv(o.height);
            }
            o.height = v;
            v = 0;
            if (sv(o.width)) {
                v = pw;
                if (hp(e)) {
                    pa = e.parent();
                    v = pa.width();
                }
                if (o.width !== a.a) {
                    v = Math.floor((tmp*0.01)*nv(o.width));
                }
                v = v-(nv(pa.css(a.m+a.l))+nv(pa.css(a.m+a.r))+nv(pa.css(a.p+a.l))+nv(pa.css(a.p+a.r))+nv(pa.css(a.b+a.l+a.w))+nv(pa.css(a.b+a.r+a.w)));
            } else {
                v = nv(o.width);
            }
            o.width = v;
            if (o.height < 100) { o.height = 100; }
            if (o.width < 200)  { o.width  = 200; }
            c = t.csn;
            k = e.clone();

            /* create a new container for the TinyTbl */
            var d1 = '<div class="', d2 ='">', d3 = '</div>';
            d = $(d1+c.tbl+(o.rtl ? '-rtl':'')+(e.attr('class') ? ' '+e.attr('class') : '')+(e.attr('id') ? '" role="'+e.attr('id') : '')+d2+d3);

            /* make the original table empty and hide it */
            e.empty().css({ 'display':'none' });

            /* append the above created container to the DOM after the original table */
            e.after(d);

            s = {
                tb:(o.tbodycss ? ' ' + o.tbodycss : ''), tf:(o.tfootcss ? ' ' + o.tfootcss : ''), th:(o.theadcss ? ' ' + o.theadcss : ''),
                tc:d1+c.tbl+'-content'+d2+d3, fl:' style="float:'+(o.rtl ? 'right':'left'), cb:'<div style="clear:both">'+d3,
            };

            /* append the above created container some orther containers to make table scrollable */
            s.div = '';
            if (o.th) {
                s.div+=d1+c.th+d2+d1+c.th+'-left'+s.th+'"'+s.fl+d2+s.tc+d3+d1+c.th+'-right'+s.th+'"'+s.fl+d2+s.tc+d3+s.cb+d3;
            }
            s.div+=d1+c.tb+d2+d1+c.tb+'-left'+s.tb+'"'+s.fl+d2+s.tc+d3+d1+c.tb+'-right'+s.tb+'"'+s.fl+d2+s.tc+d3+s.cb+d3;
            if (o.tf) {
                s.div+=d1+c.tf+d2+d1+c.tf+'-left'+s.tf+'"'+s.fl+d2+s.tc+d3+d1+c.tf+'-right'+s.tf+'"'+s.fl+d2+s.tc+d3+s.cb+d3;
            }
            d.append(s.div);

            /* store each element of the new container into a global object */
            s = { l:'-left', r:'-right',c:' .'+c.tbl+'-content', h:'.'+c.th, f:'.'+c.tf, b:'.'+c.tb };
            x = $.extend(x, {
                tbl: d,
                th0: $(s.h, d), th1: $(s.h+s.l, d), th3:$(s.h+s.l+s.c, d), th2:$(s.h+s.r, d), th4:$(s.h+s.r+s.c, d),
                tb0: $(s.b, d), tb1: $(s.b+s.l, d), tb3:$(s.b+s.l+s.c, d), tb2:$(s.b+s.r, d), tb4:$(s.b+s.r+s.c, d),
                tf0: $(s.f, d), tf1: $(s.f+s.l, d), tf3:$(s.f+s.l+s.c, d), tf2:$(s.f+s.r, d), tf4:$(s.f+s.r+s.c, d),
                cln: k, opt: o
            });
            e.data(x);

            t._ti();
            s = 'hidden';
            s = {x:{'overflow-x':s}, a:{'overflow':s}};
            x.th2.css(s.x);
            x.tf2.css(s.x);
            x.tb1.css(s.a);
            x.tb2.css({'overflow':'auto'}).scroll(function(a) { t._ts(a); });
            t._td();
            if (o.focus) {
                x.tb2.focus();
            }
        },

        /**
         * public functions
         */
        append:  function(a) { this._ra('append',a); },
        prepend: function(a) { this._ra('prepend',a); },
        remove:  function(a) { this._rd(a); },

        /**
         * public function to destroy the TinyTBL
         */
        destroy: function() {
            var e = this.element, o = e.data('tbl'), h = e.data('cln').html();
            e.html(h);
            e.css({'display':'block'});
            o.remove();
            e.removeData();
        },

        /**
         * public function to set focus to the scrollable area
         */
        focus: function() {
            var d = this.element.data();
            d.tb2.focus();
        }

    });

    $.extend($.ui.tinytbl, { version: '1.9.0a' });

})(jQuery);

